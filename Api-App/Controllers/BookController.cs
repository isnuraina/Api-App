using Api_App.Data;
using Api_App.DTOs.Book;
using Api_App.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public BookController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]BookCreateDto request)
        {
            await _context.Books.AddAsync(_mapper.Map<Book>(request));
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Create),"Created success");
        }
    }
}
