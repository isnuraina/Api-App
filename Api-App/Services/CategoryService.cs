using Api_App.Data;
using Api_App.DTOs.Category;
using Api_App.Helpers.Exceptions;
using Api_App.Models;
using Api_App.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_App.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public CategoryService(AppDbContext context, IMapper mapper, IFileService fileService)
        {
            _context = context;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null) throw new NotFoundException("Category not found");
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<IEnumerable<CategoryDto>> SearchAsync(string keyword)
        {
            var query = _context.Categories
                .Where(c => c.Name.ToLower().Contains(keyword.ToLower()));
            return _mapper.Map<IEnumerable<CategoryDto>>(await query.ToListAsync());
        }

        public async Task<IEnumerable<CategoryDto>> SortAsync(string sortBy)
        {
            var query = _context.Categories.AsQueryable();

            switch (sortBy.ToLower())
            {
                case "name":
                    query = query.OrderBy(c => c.Name);
                    break;
                default:
                    break;
            }

            return _mapper.Map<IEnumerable<CategoryDto>>(await query.ToListAsync());
        }

        public async Task CreateAsync(CategoryCreateDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Image = await _fileService.UploadFileAsync(dto.Image, "UploadFiles/CategoryImages")
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, CategoryEditDto dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null) throw new NotFoundException("Category not found");

            category.Name = dto.Name;

            if (dto.Image != null)
            {
                _fileService.Delete(category.Image, "CategoryImages");
                category.Image = await _fileService.UploadFileAsync(dto.Image, "UploadFiles/CategoryImages");
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null) throw new NotFoundException("Category not found");

            _fileService.Delete(category.Image, "UploadFiles/CategoryImages");
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }

}
