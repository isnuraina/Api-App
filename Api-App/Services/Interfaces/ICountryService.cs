using Api_App.DTOs.Country;

namespace Api_App.Services.Interfaces
{
    public interface ICountryService
    {
        Task CreateAsync(CountryCreateDto model);
        Task<IEnumerable<CountryDto>> GetAllAsync();
        Task<CountryDto> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<CountryDto>> SearchAsync(string searchText);
        Task<bool> EditAsync(int id, CountryEditDto request);


    }
}
