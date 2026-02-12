using Application.Books.Queries;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using Application.DTOs;

namespace Test.Application.Books.Queries;

public class GetBookByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_LibroExiste_DevuelveBookDtoConNombreAutor()
    {
        Author author = Author.Create("Gabriel García Márquez", new DateTime(1927, 3, 6), "Aracataca", "garcia@example.com");
        Book book = Book.Create("Cien años de soledad", 1967, Guid.NewGuid(), 471, author.Id);
        Mock<IBookRepository> bookRepoMock = new();
        bookRepoMock.Setup(r => r.GetByIdAsync(book.Id, It.IsAny<CancellationToken>())).ReturnsAsync(book);
        Mock<IAuthorRepository> authorRepoMock = new();
        authorRepoMock.Setup(r => r.GetByIdAsync(author.Id, It.IsAny<CancellationToken>())).ReturnsAsync(author);
        GetBookByIdQueryHandler handler = new(bookRepoMock.Object, authorRepoMock.Object);

        BookDto? result = await handler.Handle(new GetBookByIdQuery(book.Id), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(book.Id, result.Id);
        Assert.Equal("Cien años de soledad", result.Title);
        Assert.Equal("Gabriel García Márquez", result.AuthorName);
    }

    [Fact]
    public async Task Handle_LibroNoExiste_DevuelveNull()
    {
        Guid id = Guid.NewGuid();
        Mock<IBookRepository> bookRepoMock = new();
        bookRepoMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync((Book?)null);
        Mock<IAuthorRepository> authorRepoMock = new();
        GetBookByIdQueryHandler handler = new(bookRepoMock.Object, authorRepoMock.Object);

        BookDto? result = await handler.Handle(new GetBookByIdQuery(id), CancellationToken.None);

        Assert.Null(result);
    }
}