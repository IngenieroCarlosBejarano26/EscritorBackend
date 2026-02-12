namespace Domain.Entities;

public class Author
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; } = string.Empty;
    public DateTime BirthDate { get; private set; }
    public string CityOfOrigin { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    private readonly List<Book> _books = [];
    public IReadOnlyCollection<Book> Books => _books.AsReadOnly();

    private Author()
    { }

    public static Author Create(string fullName, DateTime birthDate, string cityOfOrigin, string email)
    {
        return new Author
        {
            Id = Guid.NewGuid(),
            FullName = fullName.Trim(),
            BirthDate = birthDate,
            CityOfOrigin = cityOfOrigin.Trim(),
            Email = email.Trim().ToLowerInvariant(),
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string fullName, DateTime birthDate, string cityOfOrigin, string email)
    {
        FullName = fullName.Trim();
        BirthDate = birthDate;
        CityOfOrigin = cityOfOrigin.Trim();
        Email = email.Trim().ToLowerInvariant();
    }
}