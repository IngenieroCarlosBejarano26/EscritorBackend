using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddGenreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedAt", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 11, 20, 50, 26, 250, DateTimeKind.Utc).AddTicks(1057), "Obras de imaginación no basadas en hechos reales", "Ficción" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 11, 20, 50, 26, 250, DateTimeKind.Utc).AddTicks(1062), "Obras basadas en hechos reales y eventos", "No Ficción" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 2, 11, 20, 50, 26, 250, DateTimeKind.Utc).AddTicks(1064), "Novelas de crimen y suspenso", "Misterio" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2026, 2, 11, 20, 50, 26, 250, DateTimeKind.Utc).AddTicks(1065), "Historias futuristas y tecnológicas", "Ciencia Ficción" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 2, 11, 20, 50, 26, 250, DateTimeKind.Utc).AddTicks(1067), "Mundos mágicos e imaginarios", "Fantasía" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2026, 2, 11, 20, 50, 26, 250, DateTimeKind.Utc).AddTicks(1069), "Historias de amor y relaciones", "Romance" },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2026, 2, 11, 20, 50, 26, 250, DateTimeKind.Utc).AddTicks(1071), "Obras que buscan asustar e inquietar", "Terror" },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2026, 2, 11, 20, 50, 26, 250, DateTimeKind.Utc).AddTicks(1074), "Obras en verso con valor literario", "Poesía" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
