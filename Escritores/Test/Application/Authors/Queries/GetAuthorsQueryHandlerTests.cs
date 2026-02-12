using Application.Authors.Queries;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Test.Application.Authors.Queries;

public class GetAuthorsQueryHandlerTests
{
    [Fact]
    public async Task Handle_DevuelveListaDeAutoresMapeadaADto()
    {
        List<Author> authors =
        [
            Author.Create("Autor Uno", new DateTime(1950, 1, 1), "Ciudad A", "a@mail.com"),
            Author.Create("Autor Dos", new DateTime(1960, 2, 2), "Ciudad B", "b@mail.com")
        ];
        Mock<IAuthorRepository> repoMock = new();
        repoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(authors);
        GetAuthorsQueryHandler handler = new(repoMock.Object);

        IReadOnlyList<AuthorDto> result = await handler.Handle(new GetAuthorsQuery(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Autor Uno", result[0].FullName);
        Assert.Equal("Autor Dos", result[1].FullName);
    }

    [Fact]
    public async Task Handle_SinAutores_DevuelveListaVacia()
    {
        Mock<IAuthorRepository> repoMock = new();
        repoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync([]);
        GetAuthorsQueryHandler handler = new(repoMock.Object);

        IReadOnlyList<AuthorDto> result = await handler.Handle(new GetAuthorsQuery(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result);
    }
}