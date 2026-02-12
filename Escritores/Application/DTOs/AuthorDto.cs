namespace Application.DTOs;

public record AuthorDto(
    Guid Id,
    string FullName,
    DateTime BirthDate,
    string CityOfOrigin,
    string Email,
    DateTime CreatedAt);