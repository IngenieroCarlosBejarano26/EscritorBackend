namespace Application.Genres.Queries;

public class GetGenreByIdQueryHandler(IGenreRepository repository) : IRequestHandler<GetGenreByIdQuery, GenreDto?>
{
    private readonly IGenreRepository _repository = repository;

    public async Task<GenreDto?> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
    {
        var genre = await _repository.GetByIdAsync(request.Id, cancellationToken);

        return genre is null
            ? null
            : new GenreDto(genre.Id, genre.Name, genre.Description, genre.CreatedAt);
    }
}
