using Application.Books.Commands;
using FluentValidation.TestHelper;

namespace Test.Application.Books.Commands;

public class RegisterBookCommandValidatorTests
{
    private readonly RegisterBookCommandValidator _validator = new();
    private static readonly Guid AuthorId = Guid.NewGuid();

    [Fact]
    public void Title_Vacio_DeberiaTenerError()
    {
        RegisterBookCommand command = new("", 2000, Guid.NewGuid(), 100, AuthorId);
        TestValidationResult<RegisterBookCommand> result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Title);
    }

    [Fact]
    public void Title_Excede500Caracteres_DeberiaTenerError()
    {
        RegisterBookCommand command = new(new string('A', 501), 2000, Guid.NewGuid(), 100, AuthorId);
        TestValidationResult<RegisterBookCommand> result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Title);
    }

    [Fact]
    public void Year_FueraDeRango_DeberiaTenerError()
    {
        RegisterBookCommand command = new("Título", 0, Guid.NewGuid(), 100, AuthorId);
        TestValidationResult<RegisterBookCommand> result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Year);
    }

    [Fact]
    public void Year_MayorA9999_DeberiaTenerError()
    {
        RegisterBookCommand command = new("Título", 10000, Guid.NewGuid(), 100, AuthorId);
        TestValidationResult<RegisterBookCommand> result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Year);
    }

    [Fact]
    public void Genre_Vacio_DeberiaTenerError()
    {
        RegisterBookCommand command = new("Título", 2000, Guid.Empty, 100, AuthorId);
        TestValidationResult<RegisterBookCommand> result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.GenreId);
    }

    [Fact]
    public void NumberOfPages_CeroONegativo_DeberiaTenerError()
    {
        RegisterBookCommand command = new("Título", 2000, Guid.NewGuid(), 0, AuthorId);
        TestValidationResult<RegisterBookCommand> result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.NumberOfPages);
    }

    [Fact]
    public void AuthorId_Vacio_DeberiaTenerError()
    {
        RegisterBookCommand command = new("Título", 2000, Guid.NewGuid(), 100, Guid.Empty);
        TestValidationResult<RegisterBookCommand> result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.AuthorId);
    }

    [Fact]
    public void ComandoValido_NoDeberiaTenerErrores()
    {
        RegisterBookCommand command = new("Cien años de soledad", 1967, Guid.NewGuid(), 471, AuthorId);
        TestValidationResult<RegisterBookCommand> result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}