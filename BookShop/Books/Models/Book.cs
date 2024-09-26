namespace BookShop.Features.Books;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int PublicationYear { get; set; }
    public string ISBN { get; set; }
    public int InStock { get; set; }
}