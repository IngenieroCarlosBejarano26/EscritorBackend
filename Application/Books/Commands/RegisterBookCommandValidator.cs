namespace Application.Books.Commands;

public class RegisterBookCommandValidator : AbstractValidator<RegisterBookCommand>
{
    public RegisterBookCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("El título es obligatorio.")
            .MaximumLength(500).WithMessage("El título no puede exceder 500 caracteres.");

        RuleFor(x => x.Year)
            .InclusiveBetween(1, 9999).WithMessage("El año debe estar entre 1 y 9999.");

        RuleFor(x => x.GenreId)
            .NotEmpty().WithMessage("El género es obligatorio.")
            .Must(g => g != Guid.Empty).WithMessage("El género es obligatorio.");

        RuleFor(x => x.NumberOfPages)
            .GreaterThan(0).WithMessage("El número de páginas debe ser mayor a cero.");

        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("El autor es obligatorio.");
    }
}