namespace Application.Genres.Queries;

public record GetGenreByIdQuery(Guid Id) : IRequest<GenreDto?>;
