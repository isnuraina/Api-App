namespace Api_App.DTOs.Book
{
    public class BookCreateDto
    {
        public string Name { get; set; }
        public List<int> AuthorIds { get; set; }
    }
}
