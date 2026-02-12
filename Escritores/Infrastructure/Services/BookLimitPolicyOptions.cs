namespace Infrastructure.Services;

public class BookLimitPolicyOptions
{
    public const string SectionName = "BookPolicy";
    public int MaxBooksAllowed { get; set; } = 3;
}