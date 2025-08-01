using Api_App.Data;
using Api_App.DTOs.Book;
using Api_App.Models;
using Api_App.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_App.Services
{
    public class BookService:IBookService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BookService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> GetByIdAsync(int id)
        {
            var book = await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            return book == null ? null : _mapper.Map<BookDto>(book);
        }

        public async Task CreateAsync(BookCreateDto dto)
        {
            var book = _mapper.Map<Book>(dto);
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EditAsync(int id, BookEditDto dto)
        {
            var book = await _context.Books
                .Include(b => b.BookAuthors)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return false;

            book.Name = dto.Name;

            var oldAuthors = await _context.BookAuthors.Where(ba => ba.BookId == id).ToListAsync();
            _context.BookAuthors.RemoveRange(oldAuthors);

            book.BookAuthors = dto.AuthorIds.Select(aid => new BookAuthor
            {
                BookId = id,
                AuthorId = aid
            }).ToList();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books
                .Include(b => b.BookAuthors)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return false;

            _context.BookAuthors.RemoveRange(book.BookAuthors);
            _context.Books.Remove(book);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
