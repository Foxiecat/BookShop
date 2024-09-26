using BookShop.Books.Interfaces;
using BookShop.Features.Books;
using MySql.Data.MySqlClient;

namespace BookShop.Books.Repositories;

public class BookMySqlDatabase(IConfiguration configuration) : IBookRepository
{
    private readonly string? _connectionString = configuration.GetConnectionString("DefaultConnection");

    // CRUD:
    
    // Create
    public async Task<Book?> AddAsync(Book book)
    {
        await using MySqlConnection connection = new(_connectionString);
        connection.Open();

        var query = "INSERT INTO Book (Title, Author, PublicationYear, ISBN, InStock) VALUE (@title, @author, @publicationYear, @isbn, @inStock)";
        
        MySqlCommand command = new(query, connection);
        command.Parameters.AddWithValue("@title", book.Title);
        command.Parameters.AddWithValue("@author", book.Author);
        command.Parameters.AddWithValue("@publicationYear", book.PublicationYear);
        command.Parameters.AddWithValue("@isbn", book.ISBN);
        command.Parameters.AddWithValue("@inStock", book.InStock);
        
        await command.ExecuteNonQueryAsync();

        command.CommandText = "SELECT LAST_INSERT_ID()";
        return book;
    }

    // Read
    public async Task<Book?> GetByIdAsync(int id)
    {
        await using MySqlConnection connection = new(_connectionString);
        connection.Open();
        
        string query = "SELECT * FROM Book where id = @id";
        MySqlCommand command = new(query, connection);
        command.Parameters.AddWithValue("@id", id);
        
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            return new Book
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Author = reader.GetString(2),
                PublicationYear = reader.GetInt32(3),
                ISBN = reader.GetString(4),
                InStock = reader.GetInt32(5)
            };
            
        }
        
        return null;
    }

    // Read
    public async Task<ICollection<Book?>> GetAllAsync(string? title, string? author, int? publicationYear)
    {
        List<Book?> bookList = [];
        await using MySqlConnection connection = new(_connectionString);
        connection.Open();
        
        string query = "SELECT * FROM Book where 1+1=2";
        
        // Add the parameters to query if they exists
        if (!string.IsNullOrEmpty(title))
            query += " AND Title LIKE @Title";
        if (!string.IsNullOrEmpty(author))
            query += " AND Author LIKE @Author";
        if (publicationYear.HasValue)
            query += " AND PublicationYear = @PublicationYear";
        
        MySqlCommand command = new(query, connection);
        
        // Adding the parameter values if needed
        if (!string.IsNullOrEmpty(title))
            command.Parameters.AddWithValue("@Title", "%" + title + "%");
        if (!string.IsNullOrEmpty(author))
            command.Parameters.AddWithValue("@Author", "%" + author + "%");
        if (publicationYear.HasValue)
            command.Parameters.AddWithValue("@PublicationYear", publicationYear.Value);
        
        await using var reader = command.ExecuteReader();
        while (await reader.ReadAsync())
        {
            Book book = new()
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Author = reader.GetString(2),
                PublicationYear = reader.GetInt32(3),
                ISBN = reader.GetString(4),
                InStock = reader.GetInt32(5)
            };
            bookList.Add(book);
        }
        
        return bookList;
    }

    // Update
    public async Task<Book?> UpdateAsync(int id, Book book)
    {
        await using MySqlConnection connection = new(_connectionString);
        connection.Open();
        
        string query = "UPDATE Book SET Title=@Title, Author=@Author, PublicationYear=@PublicationYear, ISBN=@ISBN, InStock=@InStock WHERE Id = @Id";
        MySqlCommand command = new(query, connection);

        command.Parameters.AddWithValue("@Id", id);
        command.Parameters.AddWithValue("@Title", book.Title);
        command.Parameters.AddWithValue("@Author", book.Author);
        command.Parameters.AddWithValue("@PublicationYear", book.PublicationYear);
        command.Parameters.AddWithValue("@ISBN", book.ISBN);
        command.Parameters.AddWithValue("@InStock", book.InStock);
        
        
        int affectedRows = await command.ExecuteNonQueryAsync();
        if (affectedRows == 0)
            return null;
        
        return await GetByIdAsync(id);
    }

    // Delete
    public async Task<Book?> RemoveAsync(int id)
    {
        Book? bookToRemove = await GetByIdAsync(id);
        
        await using MySqlConnection connection = new(_connectionString);
        connection.Open();
        
        string query = "DELETE FROM Book WHERE Id = @Id";
        
        MySqlCommand command = new(query, connection);
        command.Parameters.AddWithValue("@Id", id);
        
        int affectedRows = await command.ExecuteNonQueryAsync();
        if (affectedRows > 0)
            return bookToRemove;
        
        return null;
    }
}