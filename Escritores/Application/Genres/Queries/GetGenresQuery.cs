namespace Application.Genres.Queries;

public record GetGenresQuery : IRequest<IReadOnlyList<GenreDto>>;
