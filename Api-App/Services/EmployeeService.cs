using Api_App.Data;
using Api_App.DTOs.Employee;
using Api_App.Helpers.Exceptions;
using Api_App.Models;
using Api_App.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace Api_App.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public EmployeeService(AppDbContext context, IMapper mapper, IFileService fileService)
        {
            _context = context;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task CreateAsync(EmployeeCreateDto model)
        {
            var entity = _mapper.Map<Employee>(model);
            entity.Image = await _fileService.UploadFileAsync(model.Image, "UploadFiles");
            await _context.Employees.AddAsync(entity);
            await _context.SaveChangesAsync();
           
           
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
            {
                throw new NotFoundException($"Employee with ID {id} not found");
            }
            _fileService.Delete(employee.Image, "UploadFiles");
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
           return _mapper.Map<IEnumerable<EmployeeDto>>(await _context.Employees.ToListAsync());
        }
      
        public async Task<EmployeeDto> GetById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                throw new NotFoundException($"Employee with ID {id} not found");

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task EditAsync(int id, EmployeeEditDto model)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null) throw new NotFoundException("Employee not found");
            employee.FullName = model.FullName;
            if (model.Image != null)
            {
                _fileService.Delete(employee.Image, "UploadFiles");

                employee.Image = await _fileService.UploadFileAsync(model.Image, "UploadFiles");
            }
            await _context.SaveChangesAsync();
        }
    }
}
