using Api_App.Data;
using Api_App.DTOs.Author;
using Api_App.Models;
using Api_App.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthorService _authorService;

        public AuthorController(AppDbContext context, IMapper mapper, IAuthorService authorService)
        {
            _context = context;
            _mapper = mapper;
            _authorService = authorService;
        }

       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            return author is null ? NotFound() : Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorCreateDto request)
        {
            var result = await _authorService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] AuthorEditDto request)
        {
            var success = await _authorService.EditAsync(id, request);
            return success ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var success = await _authorService.DeleteAsync(id);
            return success ? Ok() : NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _authorService.GetAllAsync();
            return Ok(result);
        }


    }
}
