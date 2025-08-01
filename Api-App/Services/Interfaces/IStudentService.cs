using Api_App.DTOs.Student;

namespace Api_App.Services.Interfaces
{
    public interface IStudentService
    {
        Task CreateAsync(StudentCreateDto model);
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task<StudentDto> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> EditAsync(int id, StudentEditDto model);
    }
}
