using Api_App.DTOs.City;
using Api_App.Models;

namespace Api_App.Services.Interfaces
{
    public interface ICityService
    {
        Task CreateAsync(CityCreateDto model);
        Task<IEnumerable<CityDto>> GetAllAsync();
        Task<CityDto> GetByIdAsync(int id);
        Task<bool> EditAsync(int id, CityEditDto request);
        Task<CityDto> DeleteAsync(int id);

    }
}
