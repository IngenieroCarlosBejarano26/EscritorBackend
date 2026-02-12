using Domain.Entities;

namespace Test.Domain.Entities;

public class AuthorTests
{
    [Fact]
    public void Create_ConDatosValidos_AsignaPropiedadesCorrectamente()
    {
        string fullName = "Gabriel García Márquez";
        DateTime birthDate = new(1927, 3, 6);
        string cityOfOrigin = "Aracataca";
        string email = "Garcia@Escritor.COM";

        Author author = Author.Create(fullName, birthDate, cityOfOrigin, email);

        Assert.NotEqual(Guid.Empty, author.Id);
        Assert.Equal("Gabriel García Márquez", author.FullName);
        Assert.Equal(birthDate, author.BirthDate);
        Assert.Equal("Aracataca", author.CityOfOrigin);
        Assert.Equal("garcia@escritor.com", author.Email);
        Assert.True((DateTime.UtcNow - author.CreatedAt).TotalSeconds < 2);
    }

    [Fact]
    public void Create_ConEspaciosEnBlanco_RecortaYNormalizaEmail()
    {
        Author author = Author.Create("  Isabel Allende  ", new DateTime(1942, 8, 2), "  Lima  ", "  ISABEL@mail.com  ");

        Assert.Equal("Isabel Allende", author.FullName);
        Assert.Equal("Lima", author.CityOfOrigin);
        Assert.Equal("isabel@mail.com", author.Email);
    }

    [Fact]
    public void Update_ModificaPropiedadesCorrectamente()
    {
        Author author = Author.Create("Autor Original", new DateTime(1950, 1, 1), "Ciudad", "autor@mail.com");
        author.Update("Autor Actualizado", new DateTime(1960, 5, 5), "Otra Ciudad", "nuevo@mail.com");

        Assert.Equal("Autor Actualizado", author.FullName);
        Assert.Equal(new DateTime(1960, 5, 5), author.BirthDate);
        Assert.Equal("Otra Ciudad", author.CityOfOrigin);
        Assert.Equal("nuevo@mail.com", author.Email);
    }
}