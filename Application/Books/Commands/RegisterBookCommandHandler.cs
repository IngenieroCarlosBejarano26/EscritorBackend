namespace Application.Books.Commands;

public class RegisterBookCommandHandler(
    IBookRepository bookRepository,
    IAuthorRepository authorRepository,
    IBookLimitPolicy bookLimitPolicy) : IRequestHandler<RegisterBookCommand, BookDto>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IBookLimitPolicy _bookLimitPolicy = bookLimitPolicy;

    public async Task<BookDto> Handle(RegisterBookCommand request, CancellationToken cancellationToken)
    {
        bool authorExists = await _authorRepository.ExistsAsync(request.AuthorId, cancellationToken);
        if (!authorExists)
            throw new AuthorNotFoundException(request.AuthorId);

        int currentCount = await _bookRepository.CountAsync(cancellationToken);
        if (currentCount >= _bookLimitPolicy.MaxBooksAllowed)
            throw new MaxBooksReachedException();

        Author? author = await _authorRepository.GetByIdAsync(request.AuthorId, cancellationToken);
        Book book = Book.Create(
            request.Title,
            request.Year,
            request.GenreId,
            request.NumberOfPages,
            request.AuthorId);

        Book created = await _bookRepository.AddAsync(book, cancellationToken);

        return new BookDto(
            created.Id,
            created.Title,
            created.Year,
            created.GenreId,
            created.NumberOfPages,
            created.AuthorId,
            author!.FullName,
            created.CreatedAt);
    }
}