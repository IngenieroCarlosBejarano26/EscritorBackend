using Domain.Entities;

namespace Test.Domain.Entities;

public class BookTests
{
    [Fact]
    public void Create_ConDatosValidos_AsignaPropiedadesCorrectamente()
    {
        Guid authorId = Guid.NewGuid();
        string title = "Cien años de soledad";
        int year = 1967;
        Guid genreId = Guid.NewGuid();
        int numberOfPages = 471;

        Book book = Book.Create(title, year, genreId, numberOfPages, authorId);

        Assert.NotEqual(Guid.Empty, book.Id);
        Assert.Equal("Cien años de soledad", book.Title);
        Assert.Equal(year, book.Year);
        Assert.Equal(genreId, book.GenreId);
        Assert.Equal(numberOfPages, book.NumberOfPages);
        Assert.Equal(authorId, book.AuthorId);
        Assert.True((DateTime.UtcNow - book.CreatedAt).TotalSeconds < 2);
    }

    [Fact]
    public void Create_ConTituloConEspacios_Recorta()
    {
        Book book = Book.Create("  El amor en los tiempos del cólera  ", 1985, Guid.NewGuid(), 348, Guid.NewGuid());

        Assert.Equal("El amor en los tiempos del cólera", book.Title);
    }

    [Fact]
    public void Update_ModificaPropiedadesCorrectamente()
    {
        Guid authorId = Guid.NewGuid();
        Guid originalGenre = Guid.NewGuid();
        Guid newGenre = Guid.NewGuid();
        Book book = Book.Create("Título Original", 2000, originalGenre, 100, authorId);
        book.Update("Título Actualizado", 2001, newGenre, 200);

        Assert.Equal("Título Actualizado", book.Title);
        Assert.Equal(2001, book.Year);
        Assert.Equal(newGenre, book.GenreId);
        Assert.Equal(200, book.NumberOfPages);
        Assert.Equal(authorId, book.AuthorId);
    }
}