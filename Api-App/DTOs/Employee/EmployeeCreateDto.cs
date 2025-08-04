namespace Api_App.DTOs.Employee
{
    public class EmployeeCreateDto
    {
        public string FullName { get; set; }
        public IFormFile Image { get; set; }
    }
}
