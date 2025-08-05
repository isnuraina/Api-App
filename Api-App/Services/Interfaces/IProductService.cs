using Api_App.DTOs.Product;

namespace Api_App.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateAsync(ProductCreateDto dto);
        Task<ProductDto> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task EditAsync(int id, ProductEditDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ProductDto>> SearchAsync(string searchText);
        Task<IEnumerable<ProductDto>> SortAsync(string orderBy); 
    }
}
