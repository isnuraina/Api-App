using Api_App.Data;
using Api_App.DTOs.Student;
using Api_App.Models;
using Api_App.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IStudentService _studentService;
        public StudentController(AppDbContext context, IStudentService studentService)
        {
            _context = context;
            _studentService = studentService;

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _studentService.CreateAsync(request);
            return Ok("Student created successfully");
        }
        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var deletedStudent = await _studentService.DeleteAsync(id);
            return deletedStudent ? Ok("Deleted") : NotFound();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            return student == null ? NotFound() : Ok(student);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute]int id,[FromBody]Student request)
        {
            var data = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            if (data is null)
            {
                return NotFound();
            }
            data.FullName = request.FullName;
            data.Email = request.Email;
            data.Age = request.Age;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
