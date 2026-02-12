namespace Application.Authors.Queries;

public record GetAuthorsQuery : IRequest<IReadOnlyList<AuthorDto>>;