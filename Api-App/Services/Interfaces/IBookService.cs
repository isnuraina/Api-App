using Api_App.DTOs.Book;

namespace Api_App.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<BookDto> GetByIdAsync(int id);
        Task CreateAsync(BookCreateDto dto);
        Task<bool> EditAsync(int id, BookEditDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
