using Domain.Exceptions;

namespace Test.Domain.Exceptions;

public class DomainExceptionTests
{
    [Fact]
    public void AuthorNotFoundException_SinParametros_DevuelveMensajeEsperado()
    {
        AuthorNotFoundException ex = new();
        Assert.Equal("El autor no está registrado.", ex.Message);
    }

    [Fact]
    public void AuthorNotFoundException_ConAuthorId_DevuelveMensajeConId()
    {
        Guid authorId = Guid.NewGuid();
        AuthorNotFoundException ex = new(authorId);
        Assert.Contains(authorId.ToString(), ex.Message);
        Assert.IsType<AuthorNotFoundException>(ex);
        Assert.IsAssignableFrom<DomainException>(ex);
    }

    [Fact]
    public void MaxBooksReachedException_DevuelveMensajeEsperado()
    {
        MaxBooksReachedException ex = new();
        Assert.Equal("No es posible registrar el libro, se alcanzó el máximo permitido.", ex.Message);
        Assert.IsType<MaxBooksReachedException>(ex);
        Assert.IsAssignableFrom<DomainException>(ex);
    }

    [Fact]
    public void DomainException_ConMensaje_AsignaMensaje()
    {
        DomainException ex = new("Mensaje de prueba");
        Assert.Equal("Mensaje de prueba", ex.Message);
    }
}