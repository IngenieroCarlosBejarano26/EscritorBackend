namespace Application.Authors.Commands;

public class RegisterAuthorCommandHandler(IAuthorRepository authorRepository) : IRequestHandler<RegisterAuthorCommand, AuthorDto>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<AuthorDto> Handle(RegisterAuthorCommand request, CancellationToken cancellationToken)
    {
        Author author = Author.Create(
            request.FullName,
            request.BirthDate,
            request.CityOfOrigin,
            request.Email);

        Author created = await _authorRepository.AddAsync(author, cancellationToken);

        return new AuthorDto(
            created.Id,
            created.FullName,
            created.BirthDate,
            created.CityOfOrigin,
            created.Email,
            created.CreatedAt);
    }
}