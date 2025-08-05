using Api_App.DTOs.Category;

namespace Api_App.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> GetByIdAsync(int id);
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<IEnumerable<CategoryDto>> SearchAsync(string keyword);
        Task<IEnumerable<CategoryDto>> SortAsync(string sortBy);
        Task CreateAsync(CategoryCreateDto dto);
        Task EditAsync(int id, CategoryEditDto dto);
        Task DeleteAsync(int id);
    }
}
