using Application.Books.Commands;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Moq;
using Application.DTOs;

namespace Test.Application.Books.Commands;

public class RegisterBookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly Mock<IBookLimitPolicy> _bookLimitPolicyMock;
    private readonly RegisterBookCommandHandler _handler;
    private static readonly Guid AuthorId = Guid.NewGuid();
    private static readonly Author AuthorEntity = Author.Create("Gabriel García Márquez", new DateTime(1927, 3, 6), "Aracataca", "garcia@example.com");

    public RegisterBookCommandHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _bookLimitPolicyMock = new Mock<IBookLimitPolicy>();
        _handler = new RegisterBookCommandHandler(
            _bookRepositoryMock.Object,
            _authorRepositoryMock.Object,
            _bookLimitPolicyMock.Object);
    }

    [Fact]
    public async Task Handle_AutorNoExiste_LanzaAuthorNotFoundException()
    {
        _authorRepositoryMock.Setup(r => r.ExistsAsync(AuthorId, It.IsAny<CancellationToken>())).ReturnsAsync(false);
        RegisterBookCommand command = new("Cien años de soledad", 1967, Guid.NewGuid(), 471, AuthorId);

        await Assert.ThrowsAsync<AuthorNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        _bookRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_SeAlcanzoMaximoLibros_LanzaMaxBooksReachedException()
    {
        _authorRepositoryMock.Setup(r => r.ExistsAsync(AuthorId, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _bookRepositoryMock.Setup(r => r.CountAsync(It.IsAny<CancellationToken>())).ReturnsAsync(100);
        _bookLimitPolicyMock.Setup(p => p.MaxBooksAllowed).Returns(100);
        RegisterBookCommand command = new("Nuevo libro", 2020, Guid.NewGuid(), 200, AuthorId);

        await Assert.ThrowsAsync<MaxBooksReachedException>(() => _handler.Handle(command, CancellationToken.None));
        _bookRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ConDatosValidos_DevuelveBookDtoYGuardaLibro()
    {
        _authorRepositoryMock.Setup(r => r.ExistsAsync(AuthorId, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _authorRepositoryMock.Setup(r => r.GetByIdAsync(AuthorId, It.IsAny<CancellationToken>())).ReturnsAsync(AuthorEntity);
        _bookRepositoryMock.Setup(r => r.CountAsync(It.IsAny<CancellationToken>())).ReturnsAsync(5);
        _bookLimitPolicyMock.Setup(p => p.MaxBooksAllowed).Returns(100);
        Book? capturedBook = null;
        _bookRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
            .Callback<Book, CancellationToken>((b, _) => capturedBook = b)
            .ReturnsAsync((Book b, CancellationToken _) => b);

        Guid genreId = Guid.NewGuid();
        RegisterBookCommand command = new("Cien años de soledad", 1967, genreId, 471, AuthorId);
        BookDto result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(capturedBook!.Id, result.Id);
        Assert.Equal("Cien años de soledad", result.Title);
        Assert.Equal(1967, result.Year);
        Assert.Equal(genreId, result.GenreId);
        Assert.Equal(471, result.NumberOfPages);
        Assert.Equal(AuthorId, result.AuthorId);
        Assert.Equal("Gabriel García Márquez", result.AuthorName);
        _bookRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}