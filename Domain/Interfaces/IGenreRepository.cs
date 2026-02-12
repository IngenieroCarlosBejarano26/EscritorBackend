namespace Domain.Interfaces;

public interface IGenreRepository
{
    Task<Genre?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Genre>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Genre> AddAsync(Genre genre, CancellationToken cancellationToken = default);

    Task UpdateAsync(Genre genre, CancellationToken cancellationToken = default);
}
