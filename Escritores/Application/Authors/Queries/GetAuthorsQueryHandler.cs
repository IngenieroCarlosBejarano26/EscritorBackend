namespace Application.Authors.Queries;

public class GetAuthorsQueryHandler(IAuthorRepository authorRepository) : IRequestHandler<GetAuthorsQuery, IReadOnlyList<AuthorDto>>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<IReadOnlyList<AuthorDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Author> authors = await _authorRepository.GetAllAsync(cancellationToken);
        return [.. authors.Select(a => new AuthorDto(a.Id, a.FullName, a.BirthDate, a.CityOfOrigin, a.Email, a.CreatedAt))];
    }
}