using Api_App.DTOs.Author;

namespace Api_App.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAsync();
        Task<AuthorDto> GetByIdAsync(int id);
        Task<AuthorDto> CreateAsync(AuthorCreateDto request);
        Task<bool> EditAsync(int id, AuthorEditDto request);
        Task<bool> DeleteAsync(int id);

    }
}
