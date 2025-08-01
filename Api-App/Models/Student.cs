using System.ComponentModel.DataAnnotations;

namespace Api_App.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int Age { get; set; }

    }
}
