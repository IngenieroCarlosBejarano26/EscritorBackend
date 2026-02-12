namespace Application.DTOs;

public record BookDto(
    Guid Id,
    string Title,
    int Year,
    Guid GenreId,
    int NumberOfPages,
    Guid AuthorId,
    string? AuthorName,
    DateTime CreatedAt);