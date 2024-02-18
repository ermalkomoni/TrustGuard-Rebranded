namespace TrustGuard_API.Models;

public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string PublicationYear { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
}