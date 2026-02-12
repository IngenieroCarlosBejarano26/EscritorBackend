namespace Infrastructure.Persistence.Repositories;

public class GenreRepository(EscritoresDbContext context) : IGenreRepository
{
    private readonly EscritoresDbContext _context = context;

    public async Task<Genre?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _context.Genres.FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Genre>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _context.Genres.OrderBy(g => g.Name).ToListAsync(cancellationToken);

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _context.Genres.AnyAsync(g => g.Id == id, cancellationToken);

    public async Task<Genre> AddAsync(Genre genre, CancellationToken cancellationToken = default)
    {
        await _context.Genres.AddAsync(genre, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return genre;
    }

    public async Task UpdateAsync(Genre genre, CancellationToken cancellationToken = default)
    {
        _context.Genres.Update(genre);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
