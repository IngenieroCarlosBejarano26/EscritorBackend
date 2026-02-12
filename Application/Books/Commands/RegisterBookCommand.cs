namespace Application.Books.Commands;

public record RegisterBookCommand(
    string Title,
    int Year,
    Guid GenreId,
    int NumberOfPages,
    Guid AuthorId) : IRequest<BookDto>;