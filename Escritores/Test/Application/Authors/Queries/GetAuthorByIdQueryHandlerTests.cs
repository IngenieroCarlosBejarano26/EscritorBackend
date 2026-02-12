using Application.Authors.Queries;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Test.Application.Authors.Queries;

public class GetAuthorByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_AutorExiste_DevuelveAuthorDto()
    {
        Guid id = Guid.NewGuid();
        Author author = Author.Create("Gabriel García Márquez", new DateTime(1927, 3, 6), "Aracataca", "garcia@example.com");
        Mock<IAuthorRepository> repoMock = new();
        repoMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(author);
        GetAuthorByIdQueryHandler handler = new(repoMock.Object);

        AuthorDto? result = await handler.Handle(new GetAuthorByIdQuery(id), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(author.Id, result.Id);
        Assert.Equal("Gabriel García Márquez", result.FullName);
    }

    [Fact]
    public async Task Handle_AutorNoExiste_DevuelveNull()
    {
        Guid id = Guid.NewGuid();
        Mock<IAuthorRepository> repoMock = new();
        repoMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync((Author?)null);
        GetAuthorByIdQueryHandler handler = new(repoMock.Object);

        AuthorDto? result = await handler.Handle(new GetAuthorByIdQuery(id), CancellationToken.None);

        Assert.Null(result);
    }
}