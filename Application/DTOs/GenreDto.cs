namespace Application.DTOs;

public record GenreDto(
    Guid Id,
    string Name,
    string Description,
    DateTime CreatedAt);
