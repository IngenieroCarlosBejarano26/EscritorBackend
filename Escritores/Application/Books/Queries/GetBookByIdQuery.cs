namespace Application.Books.Queries;

public record GetBookByIdQuery(Guid Id) : IRequest<BookDto?>;