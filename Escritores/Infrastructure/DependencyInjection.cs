using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<BookLimitPolicyOptions>(
            configuration.GetSection(BookLimitPolicyOptions.SectionName));

        services.AddDbContext<EscritoresDbContext>(options =>
        {
            string? connectionString = configuration.GetConnectionString("DefaultConnection");
            if (!string.IsNullOrEmpty(connectionString))
                options.UseSqlServer(connectionString);
            else
                options.UseInMemoryDatabase("EscritoresDb");
        });

        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddSingleton<IBookLimitPolicy, BookLimitPolicy>();

        return services;
    }
}