using Api_App.Data;
using Api_App.DTOs.City;
using Api_App.Models;
using Api_App.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_App.Services
{
    public class CityService:ICityService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CityService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(CityCreateDto model)
        {
            await _context.Cities.AddAsync(_mapper.Map<City>(model));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CityDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<CityDto>>(await _context.Cities.Include(x => x.Country).ToListAsync());
        }

        public async Task<CityDto?> GetByIdAsync(int id)
        {
            var city = await _context.Cities
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id);

            return city is not null ? _mapper.Map<CityDto>(city) : null;
        }
        public async Task<bool> EditAsync(int id, CityEditDto request)
        {
            var city = await _context.Cities.Include(m => m.Country).FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
                return false;

            var existCountry = await _context.Countries.FirstOrDefaultAsync(x => x.Id == request.CountryId);
            if (existCountry == null)
                return false;

            _mapper.Map(request, city);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<CityDto?> DeleteAsync(int id)
        {
            var city = await _context.Cities
                .Include(m => m.Country)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (city == null)
                return null;

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return _mapper.Map<CityDto>(city);
        }

    }
}
