﻿using Api_App.Data;
using Api_App.DTOs.Book;
using Api_App.Models;
using Api_App.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBookService _service;

        public BooksController(IBookService service, AppDbContext context, IMapper mapper)
        {
            _service = service;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _service.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _service.GetByIdAsync(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookCreateDto request)
        {
            await _service.CreateAsync(request);
            return Ok("Created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] BookEditDto request)
        {
            var success = await _service.EditAsync(id, request);
            return success ? Ok("Updated successfully") : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? Ok("Deleted successfully") : NotFound();
        }
    }

}
