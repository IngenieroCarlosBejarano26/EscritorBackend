namespace Application.Genres.Commands;

public record RegisterGenreCommand(
    string Name,
    string Description) : IRequest<GenreDto>;
