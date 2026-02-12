namespace Domain.Entities;

public class Book
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public int Year { get; private set; }
    public Guid GenreId { get; private set; }
    public int NumberOfPages { get; private set; }
    public Guid AuthorId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Author Author { get; private set; } = null!;
    public Genre? Genre { get; private set; }

    private Book()
    { }

    public static Book Create(string title, int year, Guid genreId, int numberOfPages, Guid authorId)
    {
        return new Book
        {
            Id = Guid.NewGuid(),
            Title = title.Trim(),
            Year = year,
            GenreId = genreId,
            NumberOfPages = numberOfPages,
            AuthorId = authorId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string title, int year, Guid genreId, int numberOfPages)
    {
        Title = title.Trim();
        Year = year;
        GenreId = genreId;
        NumberOfPages = numberOfPages;
    }
}