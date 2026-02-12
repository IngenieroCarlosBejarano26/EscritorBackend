namespace Domain.Exceptions;

public class AuthorNotFoundException : DomainException
{
    public AuthorNotFoundException()
        : base("El autor no está registrado.") { }

    public AuthorNotFoundException(Guid authorId)
        : base($"El autor con Id '{authorId}' no está registrado.") { }
}