using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "genre",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "varchar", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genre", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "movie",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    age_restriction = table.Column<string>(type: "varchar", maxLength: 16, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    duration_minutes = table.Column<int>(type: "integer", nullable: false),
                    average_rating = table.Column<float>(type: "real", nullable: false),
                    rating_count = table.Column<int>(type: "integer", nullable: false),
                    release_date = table.Column<DateOnly>(type: "date", nullable: false),
                    tagline = table.Column<string>(type: "varchar", maxLength: 120, nullable: false),
                    title = table.Column<string>(type: "varchar", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "movie_genre",
                columns: table => new
                {
                    movie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    genre_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_genre", x => new { x.movie_id, x.genre_id });
                    table.ForeignKey(
                        name: "FK_movie_genre_genre_genre_id",
                        column: x => x.genre_id,
                        principalTable: "genre",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_genre_movie_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Rating",
                table: "movie",
                column: "average_rating");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ReleaseDate",
                table: "movie",
                column: "release_date");

            migrationBuilder.CreateIndex(
                name: "IX_movie_genre_genre_id",
                table: "movie_genre",
                column: "genre_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movie_genre");

            migrationBuilder.DropTable(
                name: "genre");

            migrationBuilder.DropTable(
                name: "movie");
        }
    }
}
