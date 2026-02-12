namespace Domain.Interfaces;

public interface IBookLimitPolicy
{
    int MaxBooksAllowed { get; }
}