using Api_App.Data;
using Api_App.DTOs.Employee;
using Api_App.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]EmployeeCreateDto request)
        {
            string fileName = Guid.NewGuid().ToString() + request.Image.FileName;
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string filePath = Path.Combine(folderPath, fileName);
            using(FileStream stream=new(filePath, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }
            var entity = _mapper.Map<Employee>(request);
            entity.Image = fileName;
            await _context.Employees.AddAsync(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Create), "Success created");
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(await _context.Employees.ToListAsync()));
        }
    }
}
