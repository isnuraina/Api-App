using Api_App.DTOs.Employee;

namespace Api_App.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task CreateAsync(EmployeeCreateDto model);
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task DeleteAsync(int id);
        Task<EmployeeDto> GetById(int id);
        Task EditAsync(int id, EmployeeEditDto dto);

    }
}
