namespace Application.Authors.Commands;

public record RegisterAuthorCommand(
    string FullName,
    DateTime BirthDate,
    string CityOfOrigin,
    string Email) : IRequest<AuthorDto>;