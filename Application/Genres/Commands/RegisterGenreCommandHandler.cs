namespace Application.Genres.Commands;

public class RegisterGenreCommandHandler(IGenreRepository repository) : IRequestHandler<RegisterGenreCommand, GenreDto>
{
    private readonly IGenreRepository _repository = repository;

    public async Task<GenreDto> Handle(RegisterGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = Genre.Create(request.Name, request.Description);
        await _repository.AddAsync(genre, cancellationToken);

        return new GenreDto(genre.Id, genre.Name, genre.Description, genre.CreatedAt);
    }
}
