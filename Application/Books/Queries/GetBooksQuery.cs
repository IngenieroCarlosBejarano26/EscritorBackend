namespace Application.Books.Queries;

public record GetBooksQuery : IRequest<IReadOnlyList<BookDto>>;