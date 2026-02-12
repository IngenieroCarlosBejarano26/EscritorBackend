using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(EscritoresDbContext))]
    [Migration("20260211211543_UseGenreIdOnBook")]
    partial class UseGenreIdOnBook
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CityOfOrigin")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Authors", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GenreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NumberOfPages")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("GenreId");

                    b.ToTable("Books", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Genres", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111111"),
                            CreatedAt = new DateTime(2026, 2, 11, 21, 15, 43, 92, DateTimeKind.Utc).AddTicks(4484),
                            Description = "Obras de imaginación no basadas en hechos reales",
                            Name = "Ficción"
                        },
                        new
                        {
                            Id = new Guid("22222222-2222-2222-2222-222222222222"),
                            CreatedAt = new DateTime(2026, 2, 11, 21, 15, 43, 92, DateTimeKind.Utc).AddTicks(4489),
                            Description = "Obras basadas en hechos reales y eventos",
                            Name = "No Ficción"
                        },
                        new
                        {
                            Id = new Guid("33333333-3333-3333-3333-333333333333"),
                            CreatedAt = new DateTime(2026, 2, 11, 21, 15, 43, 92, DateTimeKind.Utc).AddTicks(4492),
                            Description = "Novelas de crimen y suspenso",
                            Name = "Misterio"
                        },
                        new
                        {
                            Id = new Guid("44444444-4444-4444-4444-444444444444"),
                            CreatedAt = new DateTime(2026, 2, 11, 21, 15, 43, 92, DateTimeKind.Utc).AddTicks(4493),
                            Description = "Historias futuristas y tecnológicas",
                            Name = "Ciencia Ficción"
                        },
                        new
                        {
                            Id = new Guid("55555555-5555-5555-5555-555555555555"),
                            CreatedAt = new DateTime(2026, 2, 11, 21, 15, 43, 92, DateTimeKind.Utc).AddTicks(4495),
                            Description = "Mundos mágicos e imaginarios",
                            Name = "Fantasía"
                        },
                        new
                        {
                            Id = new Guid("66666666-6666-6666-6666-666666666666"),
                            CreatedAt = new DateTime(2026, 2, 11, 21, 15, 43, 92, DateTimeKind.Utc).AddTicks(4497),
                            Description = "Historias de amor y relaciones",
                            Name = "Romance"
                        },
                        new
                        {
                            Id = new Guid("77777777-7777-7777-7777-777777777777"),
                            CreatedAt = new DateTime(2026, 2, 11, 21, 15, 43, 92, DateTimeKind.Utc).AddTicks(4499),
                            Description = "Obras que buscan asustar e inquietar",
                            Name = "Terror"
                        },
                        new
                        {
                            Id = new Guid("88888888-8888-8888-8888-888888888888"),
                            CreatedAt = new DateTime(2026, 2, 11, 21, 15, 43, 92, DateTimeKind.Utc).AddTicks(4501),
                            Description = "Obras en verso con valor literario",
                            Name = "Poesía"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Book", b =>
                {
                    b.HasOne("Domain.Entities.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Domain.Entities.Author", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
