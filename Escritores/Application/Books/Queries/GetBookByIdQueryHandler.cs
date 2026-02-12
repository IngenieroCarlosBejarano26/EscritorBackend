namespace Application.Books.Queries;

public class GetBookByIdQueryHandler(IBookRepository bookRepository, IAuthorRepository authorRepository) : IRequestHandler<GetBookByIdQuery, BookDto?>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<BookDto?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        Book? book = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);
        if (book is null)
            return null;

        Author? author = await _authorRepository.GetByIdAsync(book.AuthorId, cancellationToken);
        return new BookDto(
            book.Id,
            book.Title,
            book.Year,
            book.GenreId,
            book.NumberOfPages,
            book.AuthorId,
            author?.FullName,
            book.CreatedAt);
    }
}