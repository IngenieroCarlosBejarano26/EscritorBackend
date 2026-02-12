namespace Application.Authors.Queries;

public class GetAuthorByIdQueryHandler(IAuthorRepository authorRepository) : IRequestHandler<GetAuthorByIdQuery, AuthorDto?>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<AuthorDto?> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        Author? author = await _authorRepository.GetByIdAsync(request.Id, cancellationToken);
        return author is null
            ? null
            : new AuthorDto(author.Id, author.FullName, author.BirthDate, author.CityOfOrigin, author.Email, author.CreatedAt);
    }
}