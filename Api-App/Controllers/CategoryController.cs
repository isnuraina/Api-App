using Api_App.Data;
using Api_App.DTOs.Category;
using Api_App.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateDto dto)
        {
            await _categoryService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), "Category created");
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _categoryService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _categoryService.GetByIdAsync(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CategoryEditDto dto)
        {
            await _categoryService.EditAsync(id, dto);
            return Ok("Updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return Ok("Deleted");
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string keyword) => Ok(await _categoryService.SearchAsync(keyword));

        [HttpGet]
        public async Task<IActionResult> Sort([FromQuery] string sortBy) => Ok(await _categoryService.SortAsync(sortBy));
    }
}
