namespace Api_App.DTOs.Book
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> AuthorNames { get; set; }
    }
}
