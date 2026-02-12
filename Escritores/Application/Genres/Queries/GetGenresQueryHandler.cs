namespace Application.Genres.Queries;

public class GetGenresQueryHandler(IGenreRepository repository) : IRequestHandler<GetGenresQuery, IReadOnlyList<GenreDto>>
{
    private readonly IGenreRepository _repository = repository;

    public async Task<IReadOnlyList<GenreDto>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
    {
        var genres = await _repository.GetAllAsync(cancellationToken);

        return genres
            .Select(g => new GenreDto(g.Id, g.Name, g.Description, g.CreatedAt))
            .ToList();
    }
}
