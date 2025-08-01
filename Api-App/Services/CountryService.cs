using Api_App.Data;
using Api_App.DTOs.Country;
using Api_App.Models;
using Api_App.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_App.Services
{
    public class CountryService : ICountryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CountryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(CountryCreateDto model)
        {
            await _context.Countries.AddAsync(_mapper.Map<Country>(model));
            await _context.SaveChangesAsync();   
        }

        public async Task<IEnumerable<CountryDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<CountryDto>>(await _context.Countries.ToListAsync());
        }

        public async Task<CountryDto> GetByIdAsync(int id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(m => m.Id == id);
            if (country is null)
            {
                return null;
            }
            return _mapper.Map<CountryDto>(country);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return false;
            }
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CountryDto>> SearchAsync(string searchText)
        {
            searchText = searchText?.Trim().ToLower();

            var countries = await _context.Countries
                .Where(m => m.Name.Trim().ToLower().Contains(searchText))
                .ToListAsync();

            return _mapper.Map<IEnumerable<CountryDto>>(countries);
        }
        public async Task<bool> EditAsync(int id, CountryEditDto request)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
            if (country == null)
                return false;

            _mapper.Map(request, country);
            await _context.SaveChangesAsync();
            return true;
        }



    }
}
