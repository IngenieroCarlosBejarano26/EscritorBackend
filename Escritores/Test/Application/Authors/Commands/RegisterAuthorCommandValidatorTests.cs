using Application.Authors.Commands;
using FluentValidation.TestHelper;

namespace Test.Application.Authors.Commands;

public class RegisterAuthorCommandValidatorTests
{
    private readonly RegisterAuthorCommandValidator _validator = new();
    private static readonly DateTime FechaPasada = DateTime.UtcNow.AddYears(-30);

    [Fact]
    public void FullName_Vacio_DeberiaTenerError()
    {
        RegisterAuthorCommand command = new("", FechaPasada, "Ciudad", "test@mail.com");
        TestValidationResult<RegisterAuthorCommand> result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.FullName);
        result.ShouldNotHaveValidationErrorFor(c => c.BirthDate);
    }

    [Fact]
    public void FullName_Excede200Caracteres_DeberiaTenerError()
    {
        RegisterAuthorCommand command = new(new string('A', 201), FechaPasada, "Ciudad", "test@mail.com");
        TestValidationResult<RegisterAuthorCommand> result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.FullName);
    }

    [Fact]
    public void BirthDate_Futuro_DeberiaTenerError()
    {
        RegisterAuthorCommand command = new("Autor", DateTime.UtcNow.AddDays(1), "Ciudad", "test@mail.com");
        TestValidationResult<RegisterAuthorCommand> result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.BirthDate);
    }

    [Fact]
    public void CityOfOrigin_Vacio_DeberiaTenerError()
    {
        RegisterAuthorCommand command = new("Autor", FechaPasada, "", "test@mail.com");
        TestValidationResult<RegisterAuthorCommand> result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.CityOfOrigin);
    }

    [Fact]
    public void Email_Vacio_DeberiaTenerError()
    {
        RegisterAuthorCommand command = new("Autor", FechaPasada, "Ciudad", "");
        TestValidationResult<RegisterAuthorCommand> result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Email);
    }

    [Fact]
    public void Email_FormatoInvalido_DeberiaTenerError()
    {
        RegisterAuthorCommand command = new("Autor", FechaPasada, "Ciudad", "no-es-email");
        TestValidationResult<RegisterAuthorCommand> result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Email);
    }

    [Fact]
    public void ComandoValido_NoDeberiaTenerErrores()
    {
        RegisterAuthorCommand command = new("Gabriel García Márquez", FechaPasada, "Aracataca", "garcia@example.com");
        TestValidationResult<RegisterAuthorCommand> result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}