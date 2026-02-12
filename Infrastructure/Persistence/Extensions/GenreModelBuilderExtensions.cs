namespace Infrastructure.Persistence.Extensions;

public static class GenreModelBuilderExtensions
{
    public static void SeedGenres(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new() 
            { 
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Ficción",
                Description = "Obras de imaginación no basadas en hechos reales",
                CreatedAt = DateTime.UtcNow
            },
            new() 
            { 
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "No Ficción",
                Description = "Obras basadas en hechos reales y eventos",
                CreatedAt = DateTime.UtcNow
            },
            new() 
            { 
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Misterio",
                Description = "Novelas de crimen y suspenso",
                CreatedAt = DateTime.UtcNow
            },
            new() 
            { 
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Name = "Ciencia Ficción",
                Description = "Historias futuristas y tecnológicas",
                CreatedAt = DateTime.UtcNow
            },
            new() 
            { 
                Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                Name = "Fantasía",
                Description = "Mundos mágicos e imaginarios",
                CreatedAt = DateTime.UtcNow
            },
            new() 
            { 
                Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                Name = "Romance",
                Description = "Historias de amor y relaciones",
                CreatedAt = DateTime.UtcNow
            },
            new() 
            { 
                Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                Name = "Terror",
                Description = "Obras que buscan asustar e inquietar",
                CreatedAt = DateTime.UtcNow
            },
            new() 
            { 
                Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                Name = "Poesía",
                Description = "Obras en verso con valor literario",
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}
