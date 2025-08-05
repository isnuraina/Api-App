using Api_App.Data;
using Api_App.DTOs.Employee;
using Api_App.Helpers.Exceptions;
using Api_App.Services.Interfaces;
using AutoMapper;
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

            await _employeeService.CreateAsync(request);
            return CreatedAtAction(nameof(Create), "Success created");
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _employeeService.GetAllAsync());
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                await _employeeService.DeleteAsync(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var employee = await _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] EmployeeEditDto request)
        {
            try
            {
                await _employeeService.EditAsync(id, request);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

    };
}
