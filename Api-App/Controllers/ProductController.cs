using Api_App.DTOs.Product;
using Api_App.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto dto)
        {
            await _productService.CreateAsync(dto);
            return CreatedAtAction(nameof(Create), "Product created successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ProductEditDto dto)
        {
            await _productService.EditAsync(id, dto);
            return Ok("Updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok("Deleted");
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string text)
        {
            return Ok(await _productService.SearchAsync(text));
        }

        [HttpGet]
        public async Task<IActionResult> Sort([FromQuery] string orderBy)
        {
            return Ok(await _productService.SortAsync(orderBy));
        }
    }

}
