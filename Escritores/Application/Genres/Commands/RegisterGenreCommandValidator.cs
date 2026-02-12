namespace Application.Genres.Commands;

public class RegisterGenreCommandValidator : AbstractValidator<RegisterGenreCommand>
{
    public RegisterGenreCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del género es requerido")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripción del género es requerida")
            .MaximumLength(500).WithMessage("La descripción no puede exceder 500 caracteres");
    }
}
