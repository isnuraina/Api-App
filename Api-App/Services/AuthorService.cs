using Api_App.Data;
using Api_App.DTOs.Author;
using Api_App.Models;
using Api_App.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_App.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public AuthorService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

      
        public async Task<AuthorDto> GetByIdAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return null;
            return _mapper.Map<AuthorDto>(author);
        }
        public async Task<AuthorDto> CreateAsync(AuthorCreateDto request)
        {
            var author = _mapper.Map<Author>(request);
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return _mapper.Map<AuthorDto>(author);
        }
        public async Task<bool> EditAsync(int id, AuthorEditDto request)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return false;
            _mapper.Map(request, author);
            await _context.SaveChangesAsync();
            return true;
        }

       
        public async Task<bool> DeleteAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return false;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAsync()
        {
            var authors = await _context.Authors.ToListAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

    }
}
