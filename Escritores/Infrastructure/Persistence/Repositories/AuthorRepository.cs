namespace Infrastructure.Persistence.Repositories;

public class AuthorRepository(EscritoresDbContext context) : IAuthorRepository
{
    private readonly EscritoresDbContext _context = context;

    public async Task<Author?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
    await _context.Authors.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Author>> GetAllAsync(CancellationToken cancellationToken = default) =>
    await _context.Authors.OrderBy(a => a.FullName).ToListAsync(cancellationToken);

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default) =>
    await _context.Authors.AnyAsync(a => a.Id == id, cancellationToken);

    public async Task<Author> AddAsync(Author author, CancellationToken cancellationToken = default)
    {
        await _context.Authors.AddAsync(author, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return author;
    }

    public async Task UpdateAsync(Author author, CancellationToken cancellationToken = default)
    {
        _context.Authors.Update(author);
        await _context.SaveChangesAsync(cancellationToken);
    }
}