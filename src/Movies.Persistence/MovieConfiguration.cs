using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain.MovieAggregate;
using Movies.Domain.MovieAggregate.ValueObjects;

namespace Movies.Persistence;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
	public void Configure(EntityTypeBuilder<Movie> builder)
	{
		ConfigureMoviesTable(builder);
		ConfigureMovieGenreTable(builder);
	}

	private static void ConfigureAgeRestriction(EntityTypeBuilder<Movie> builder)
	{
		builder.OwnsOne(m => m.AgeRestriction, restriction =>
		{
			restriction.Property(r => r.Value)
				.HasColumnName("age_restriction")
				.HasColumnType("varchar")
				.HasMaxLength(16);
		});
	}

	private static void ConfigureDescription(EntityTypeBuilder<Movie> builder)
	{
		builder.OwnsOne(m => m.Description, description =>
		{
			description.Property(d => d.Value)
				.HasColumnName("description")
				.HasColumnType("text")
				.IsRequired();
		});
	}

	private static void ConfigureDuration(EntityTypeBuilder<Movie> builder)
	{
		builder.OwnsOne(m => m.Duration, duration =>
		{
			duration.Property(d => d.Value)
				.HasColumnName("duration_minutes")
				.HasColumnType("integer")
				.IsRequired();
		});
	}

	private static void ConfigureMovieGenreTable(EntityTypeBuilder<Movie> builder)
	{
		builder
			.HasMany(m => m.Genres)
			.WithMany()
			.UsingEntity<Dictionary<string, object>>(
				TableNames.MovieGenres,
				j => j
					.HasOne<Genre>()
					.WithMany()
					.HasForeignKey("genre_id")
					.OnDelete(DeleteBehavior.Cascade),
				j => j
					.HasOne<Movie>()
					.WithMany()
					.HasForeignKey("movie_id")
					.OnDelete(DeleteBehavior.Cascade),
				j =>
				{
					j.HasKey("movie_id", "genre_id");
				});

		builder.Navigation(m => m.Genres).UsePropertyAccessMode(PropertyAccessMode.Field);
	}

	private static void ConfigureMoviesTable(EntityTypeBuilder<Movie> builder)
	{
		builder.ToTable(TableNames.Movies);

		builder.HasKey(m => m.Id);
		builder.Property(m => m.Id)
			.HasColumnName("id")
			.HasColumnType("uuid")
			.ValueGeneratedNever()
			.HasConversion(
				id => id.Value,
				value => MovieId.Create(value).Value);

		ConfigureTitle(builder);
		ConfigureTagline(builder);
		ConfigureDescription(builder);
		ConfigureReleaseDate(builder);
		ConfigureDuration(builder);
		ConfigureRating(builder);
		ConfigureAgeRestriction(builder);

		// Create indexes on owned entity properties using correct expression syntax
		builder.OwnsOne(m => m.ReleaseDate, rd =>
		{
			rd.HasIndex(r => r.Value).HasDatabaseName("IX_Movies_ReleaseDate");
		});

		builder.OwnsOne(m => m.Rating, r =>
		{
			r.HasIndex(rating => rating.AverageRating).HasDatabaseName("IX_Movies_Rating");
		});
	}

	private static void ConfigureRating(EntityTypeBuilder<Movie> builder)
	{
		builder.OwnsOne(m => m.Rating, rating =>
		{
			rating.Property(r => r.AverageRating)
				.HasColumnName("average_rating")
				.HasColumnType("real")
				.IsRequired();

			rating.Property(r => r.RatingCount)
				.HasColumnName("rating_count")
				.HasColumnType("integer")
				.IsRequired();
		});
	}

	private static void ConfigureReleaseDate(EntityTypeBuilder<Movie> builder)
	{
		builder.OwnsOne(m => m.ReleaseDate, releaseDate =>
		{
			releaseDate.Property(rd => rd.Value)
				.HasColumnName("release_date")
				.HasColumnType("date")
				.IsRequired();
		});
	}

	private static void ConfigureTagline(EntityTypeBuilder<Movie> builder)
	{
		// Only configure Tagline as an owned entity
		builder.OwnsOne(m => m.Tagline, tagline =>
		{
			tagline.Property(t => t.Value)
				.HasColumnName("tagline")
				.HasColumnType("varchar")
				.HasMaxLength(Tagline.MaxLength);
		});
	}

	private static void ConfigureTitle(EntityTypeBuilder<Movie> builder)
	{
		// Only configure Title as an owned entity
		builder.OwnsOne(m => m.Title, title =>
		{
			title.Property(t => t.Value)
				.HasColumnName("title")
				.HasColumnType("varchar")
				.HasMaxLength(Title.MaxLength)
				.IsRequired();
		});
	}
}