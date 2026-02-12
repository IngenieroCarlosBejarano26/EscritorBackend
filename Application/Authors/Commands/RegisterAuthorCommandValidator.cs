namespace Application.Authors.Commands;

public class RegisterAuthorCommandValidator : AbstractValidator<RegisterAuthorCommand>
{
    public RegisterAuthorCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("El nombre completo es obligatorio.")
            .MaximumLength(200).WithMessage("El nombre no puede exceder 200 caracteres.");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.")
            .LessThan(DateTime.UtcNow).WithMessage("La fecha de nacimiento debe ser anterior a la fecha actual.");

        RuleFor(x => x.CityOfOrigin)
            .NotEmpty().WithMessage("La ciudad de procedencia es obligatoria.")
            .MaximumLength(100).WithMessage("La ciudad no puede exceder 100 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
            .EmailAddress().WithMessage("El correo electrónico no tiene un formato válido.")
            .MaximumLength(256).WithMessage("El correo no puede exceder 256 caracteres.");
    }
}