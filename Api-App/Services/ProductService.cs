using Api_App.Data;
using Api_App.DTOs.Product;
using Api_App.Models;
using Api_App.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_App.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(ProductCreateDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task EditAsync(int id, ProductEditDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) throw new Exception("Product not found");

            _mapper.Map(dto, product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) throw new Exception("Product not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductDto>> SearchAsync(string searchText)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Name.ToLower().Contains(searchText.ToLower()))
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> SortAsync(string orderBy)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            if (orderBy.ToLower() == "asc")
                products = products.OrderBy(p => p.Price).ToList();
            else if (orderBy.ToLower() == "desc")
                products = products.OrderByDescending(p => p.Price).ToList();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }

}
