using System.ComponentModel.DataAnnotations;

namespace Api_App.DTOs.Student
{
    public class StudentEditDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(1, 120)]
        public int Age { get; set; }
    }
}
