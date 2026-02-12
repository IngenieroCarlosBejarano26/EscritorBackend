using Application.Books.Queries;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Test.Application.Books.Queries;

public class GetBooksQueryHandlerTests
{
    [Fact]
    public async Task Handle_DevuelveListaDeLibrosConNombreDeAutor()
    {
        var author = Author.Create("Gabriel García Márquez", new DateTime(1927, 3, 6), "Aracataca", "garcia@example.com");
        var books = new List<Book>
        {
            Book.Create("Cien años de soledad", 1967, Guid.NewGuid(), 471, author.Id)
        };
        var bookRepoMock = new Mock<IBookRepository>();
        bookRepoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(books);
        var authorRepoMock = new Mock<IAuthorRepository>();
        authorRepoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(new List<Author> { author });
        var handler = new GetBooksQueryHandler(bookRepoMock.Object, authorRepoMock.Object);

        var result = await handler.Handle(new GetBooksQuery(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Cien años de soledad", result[0].Title);
        Assert.Equal("Gabriel García Márquez", result[0].AuthorName);
    }

    [Fact]
    public async Task Handle_SinLibros_DevuelveListaVacia()
    {
        var bookRepoMock = new Mock<IBookRepository>();
        bookRepoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(new List<Book>());
        var authorRepoMock = new Mock<IAuthorRepository>();
        authorRepoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(new List<Author>());
        var handler = new GetBooksQueryHandler(bookRepoMock.Object, authorRepoMock.Object);

        var result = await handler.Handle(new GetBooksQuery(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result);
    }
}