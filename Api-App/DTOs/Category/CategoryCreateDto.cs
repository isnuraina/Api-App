namespace Api_App.DTOs.Category
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
