namespace Infrastructure.Persistence.Repositories;

public class BookRepository(EscritoresDbContext context) : IBookRepository
{
    private readonly EscritoresDbContext _context = context;

    public async Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
    await _context.Books.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Book>> GetAllAsync(CancellationToken cancellationToken = default) =>
    await _context.Books.OrderBy(b => b.Title).ToListAsync(cancellationToken);

    public async Task<int> CountAsync(CancellationToken cancellationToken = default) =>
    await _context.Books.CountAsync(cancellationToken);

    public async Task<Book> AddAsync(Book book, CancellationToken cancellationToken = default)
    {
        await _context.Books.AddAsync(book, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return book;
    }

    public async Task UpdateAsync(Book book, CancellationToken cancellationToken = default)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync(cancellationToken);
    }
}