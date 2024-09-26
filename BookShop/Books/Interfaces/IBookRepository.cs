using BookShop.Features.Books;

namespace BookShop.Books.Interfaces;

public interface IBookRepository
{
    Task<Book?> AddAsync(Book book);
    Task<Book?> GetByIdAsync(int id);
    Task<ICollection<Book?>> GetAllAsync(string? title, string? author, int? publicationYear);
    Task<Book?> UpdateAsync(int id, Book book);
    Task<Book?> RemoveAsync(int id);
}