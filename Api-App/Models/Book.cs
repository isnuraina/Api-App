namespace Api_App.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }

    }
}
