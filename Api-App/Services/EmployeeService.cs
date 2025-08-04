using Api_App.Data;
using Api_App.DTOs.Employee;
using Api_App.Models;
using Api_App.Services.Interfaces;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
           return _mapper.Map<IEnumerable<EmployeeDto>>(await _context.Employees.ToListAsync());
        }
    }
}
