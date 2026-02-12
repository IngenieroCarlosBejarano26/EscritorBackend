namespace Application.Authors.Queries;

public record GetAuthorByIdQuery(Guid Id) : IRequest<AuthorDto?>;