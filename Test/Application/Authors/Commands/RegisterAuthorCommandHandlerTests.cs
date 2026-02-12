using Application.Authors.Commands;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Test.Application.Authors.Commands;

public class RegisterAuthorCommandHandlerTests
{
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly RegisterAuthorCommandHandler _handler;

    public RegisterAuthorCommandHandlerTests()
    {
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _handler = new RegisterAuthorCommandHandler(_authorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ConDatosValidos_DevuelveAuthorDtoYGuardaAutor()
    {
        RegisterAuthorCommand command = new(
            "Gabriel García Márquez",
            new DateTime(1927, 3, 6),
            "Aracataca",
            "garcia@example.com");
        Author? capturedAuthor = null;
        _authorRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>()))
            .Callback<Author, CancellationToken>((a, _) => capturedAuthor = a)
            .ReturnsAsync((Author a, CancellationToken _) => a);

        AuthorDto result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(capturedAuthor!.Id, result.Id);
        Assert.Equal("Gabriel García Márquez", result.FullName);
        Assert.Equal(new DateTime(1927, 3, 6), result.BirthDate);
        Assert.Equal("Aracataca", result.CityOfOrigin);
        Assert.Equal("garcia@example.com", result.Email);
        _authorRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}