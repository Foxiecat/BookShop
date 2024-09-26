namespace BookShop.Models;

public class Book
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Author { get; init; }
    public int PublicationYear { get; init; }
    public string ISBN { get; init; }
    public int InStock { get; init; }
}