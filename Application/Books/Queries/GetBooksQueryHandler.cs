namespace Application.Books.Queries;

public class GetBooksQueryHandler(IBookRepository bookRepository, IAuthorRepository authorRepository) : IRequestHandler<GetBooksQuery, IReadOnlyList<BookDto>>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<IReadOnlyList<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Book> books = await _bookRepository.GetAllAsync(cancellationToken);
        List<Guid> authorIds = [.. books.Select(b => b.AuthorId).Distinct()];
        Dictionary<Guid, Author> authors = (await _authorRepository.GetAllAsync(cancellationToken))
            .Where(a => authorIds.Contains(a.Id))
            .ToDictionary(a => a.Id);

        return [.. books
            .Select(b => new BookDto(
                b.Id,
                b.Title,
                b.Year,
                b.GenreId,
                b.NumberOfPages,
                b.AuthorId,
                authors.GetValueOrDefault(b.AuthorId)?.FullName,
                b.CreatedAt))];
    }
}