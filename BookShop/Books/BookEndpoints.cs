using BookShop.Books.Interfaces;
using BookShop.Features.Books;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Books;

[Route ("/books")]
public static class BookEndpoints
{
    public static void MapBookEndpoints(this WebApplication app)
    {
        RouteGroupBuilder bookGroup = app.MapGroup("/books");
        
        bookGroup.MapGet("", GetAllBooksAsync).WithName("GetBooks").WithOpenApi();
        bookGroup.MapGet("/{id}", GetBookByIdAsync).WithName("GetBookById").WithOpenApi();
        bookGroup.MapPost("", AddBooksAsync).WithName("AddBooks").WithOpenApi();
        bookGroup.MapPut("/{id}", UpdateBookAsync).WithName("UpdateBooks").WithOpenApi();
        bookGroup.MapDelete("/{id}", DeleteBooksAsync).WithName("DeleteBooks").WithOpenApi();
    }

    
    private static async Task<IResult> GetAllBooksAsync(
        [FromServices] IBookRepository repository,
        string? title, string? author, int? publicationYear)
    {
        ICollection<Book?> books = await repository.GetAllAsync(title, author, publicationYear);
        
        // Gets from the database
        return Results.Ok(books);
    }
    
    private static async Task<IResult> GetBookByIdAsync(
        [FromServices] IBookRepository repository,
        [FromQuery] int id)
    {
        Book? book = await repository.GetByIdAsync(id);
        
        // Gets from the database
        return book is null
            ? Results.BadRequest("No books found")
            : Results.Ok(book);
    }
    
    
    private static async Task<IResult> AddBooksAsync(IBookRepository repository,
                                                ILogger<Program> logger,
                                                Book book)
    {
        logger.LogInformation("Book added: {@book}", book);
        
        Book? addBook = await repository.AddAsync(book);
        return addBook is null
            ? Results.BadRequest("Failed to add book to database")
            : Results.Ok(addBook);
    }
    
    
    private static async Task<IResult> UpdateBookAsync(IBookRepository repository,
                                                       int id,
                                                       Book book)
    {
        Book? bookToUpdate = await repository.UpdateAsync(id, book);
        return bookToUpdate is null
            ? Results.BadRequest($"Failed to update book with id={id}")
            : Results.Ok(bookToUpdate);
    }

    
    private static async Task<IResult> DeleteBooksAsync(IBookRepository repository, int id)
    {
        Book? deleteBook = await repository.RemoveAsync(id);
        return deleteBook is null
            ? Results.BadRequest($"Failed to delete book with id={id}")
            : Results.Ok(deleteBook);
    }
}