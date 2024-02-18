namespace TrustGuard_API.Models;

public class Author
{
    public int AuthorId { get; set; }
    public string Name { get; set; }
    public string BirthYear { get; set; }
    public ICollection<Book> Books { get; set; }
}