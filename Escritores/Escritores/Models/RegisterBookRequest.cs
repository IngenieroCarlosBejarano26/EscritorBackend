namespace Escritores.Models;

public record RegisterBookRequest(
    string Title,
    int Year,
    Guid GenreId,
    int NumberOfPages,
    Guid AuthorId);