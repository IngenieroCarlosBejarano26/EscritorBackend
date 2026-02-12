namespace Domain.Interfaces;

public interface IAuthorRepository
{
    Task<Author?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Author>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Author> AddAsync(Author author, CancellationToken cancellationToken = default);

    Task UpdateAsync(Author author, CancellationToken cancellationToken = default);
}