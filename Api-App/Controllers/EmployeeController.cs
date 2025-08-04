using Api_App.Data;
using Api_App.DTOs.Employee;
using Api_App.Models;
using Api_App.Services.Interfaces;
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
        private readonly IFileService _fileService;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(AppDbContext context, IMapper mapper, IFileService fileService, IEmployeeService employeeService)
        {
            _context = context;
            _mapper = mapper;
            _fileService = fileService;
            _employeeService = employeeService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]EmployeeCreateDto request)
        {

            _employeeService.CreateAsync(request);
            return CreatedAtAction(nameof(Create), "Success created");
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _employeeService.GetAllAsync());
        }
    }
}
