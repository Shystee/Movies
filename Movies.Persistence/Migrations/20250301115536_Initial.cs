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
                    created_at = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
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
                    backdrop_url = table.Column<string>(type: "varchar", maxLength: 500, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    duration_minutes = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    poster_url = table.Column<string>(type: "varchar", maxLength: 500, nullable: false),
                    average_rating = table.Column<float>(type: "real", nullable: false),
                    rating_count = table.Column<int>(type: "integer", nullable: false),
                    release_date = table.Column<DateOnly>(type: "date", nullable: false),
                    tagline = table.Column<string>(type: "varchar", maxLength: 120, nullable: false),
                    title = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    trailer_url = table.Column<string>(type: "varchar", maxLength: 500, nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "person",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    biography = table.Column<string>(type: "text", nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    photo_url = table.Column<string>(type: "varchar", maxLength: 500, nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person", x => x.id);
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

            migrationBuilder.CreateTable(
                name: "movie_cast",
                columns: table => new
                {
                    movie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_cast", x => new { x.movie_id, x.person_id });
                    table.ForeignKey(
                        name: "FK_movie_cast_movie_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_cast_person_person_id",
                        column: x => x.person_id,
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movie_director",
                columns: table => new
                {
                    movie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_director", x => new { x.movie_id, x.person_id });
                    table.ForeignKey(
                        name: "FK_movie_director_movie_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_director_person_person_id",
                        column: x => x.person_id,
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movie_writer",
                columns: table => new
                {
                    movie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_writer", x => new { x.movie_id, x.person_id });
                    table.ForeignKey(
                        name: "FK_movie_writer_movie_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_writer_person_person_id",
                        column: x => x.person_id,
                        principalTable: "person",
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
                name: "IX_movie_is_active",
                table: "movie",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "IX_movie_cast_person_id",
                table: "movie_cast",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "IX_movie_director_person_id",
                table: "movie_director",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "IX_movie_genre_genre_id",
                table: "movie_genre",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_movie_writer_person_id",
                table: "movie_writer",
                column: "person_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movie_cast");

            migrationBuilder.DropTable(
                name: "movie_director");

            migrationBuilder.DropTable(
                name: "movie_genre");

            migrationBuilder.DropTable(
                name: "movie_writer");

            migrationBuilder.DropTable(
                name: "genre");

            migrationBuilder.DropTable(
                name: "movie");

            migrationBuilder.DropTable(
                name: "person");
        }
    }
}
