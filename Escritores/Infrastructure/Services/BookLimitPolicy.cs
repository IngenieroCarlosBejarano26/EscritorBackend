namespace Infrastructure.Services;

public class BookLimitPolicy(IOptions<BookLimitPolicyOptions> options) : IBookLimitPolicy
{
    private readonly BookLimitPolicyOptions _options = options.Value;

    public int MaxBooksAllowed => _options.MaxBooksAllowed;
}