using JetBrains.Annotations;
using Movies.Domain.MovieAggregate.ValueObjects;

namespace Movies.Domain.MovieAggregate;

public sealed class Movie : AggregateRoot<MovieId, Guid>, IAuditable
{
	private readonly List<Person> _cast = [];
	private readonly List<Person> _directors = [];
	private readonly List<Genre> _genres = [];
	private readonly List<Person> _writers = [];

	private Movie(
		Title title,
		Tagline tagline,
		Description description,
		Url posterUrl,
		Url backdropUrl,
		ReleaseDate releaseDate,
		Duration duration,
		Rating rating,
		AgeRestriction ageRestriction,
		Url trailerUrl,
		bool isActive)
		: base(MovieId.CreateUnique())
	{
		Title = title;
		Tagline = tagline;
		Description = description;
		PosterUrl = posterUrl;
		BackdropUrl = backdropUrl;
		ReleaseDate = releaseDate;
		Duration = duration;
		Rating = rating;
		AgeRestriction = ageRestriction;
		TrailerUrl = trailerUrl;
		IsActive = isActive;
		CreatedAt = DateTimeOffset.UtcNow;
		UpdatedAt = DateTimeOffset.UtcNow;
	}

	[UsedImplicitly]
	private Movie()
	{
	}

	public AgeRestriction AgeRestriction { get; private set; } = null!;

	public Url BackdropUrl { get; private set; } = null!;

	public IReadOnlyList<Person> Cast => _cast.AsReadOnly();

	public DateTimeOffset CreatedAt { get; set; }

	public Description Description { get; private set; } = null!;

	public IReadOnlyList<Person> Directors => _directors.AsReadOnly();

	public Duration Duration { get; private set; } = null!;

	public IReadOnlyList<Genre> Genres => _genres.AsReadOnly();

	public bool IsActive { get; private set; }

	public Url PosterUrl { get; private set; } = null!;

	public Rating Rating { get; private set; } = null!;

	public ReleaseDate ReleaseDate { get; private set; } = null!;

	public Tagline Tagline { get; private set; } = null!;

	public Title Title { get; private set; } = null!;

	public Url TrailerUrl { get; private set; } = null!;

	public DateTimeOffset UpdatedAt { get; set; }

	public IReadOnlyList<Person> Writers => _writers.AsReadOnly();

	public static Movie Create(
		Title title,
		Tagline tagline,
		Description description,
		Url posterUrl,
		Url backdropUrl,
		ReleaseDate releaseDate,
		Duration duration,
		AgeRestriction ageRestriction,
		Url trailerUrl,
		bool isActive = true) => new(
		title,
		tagline,
		description,
		posterUrl,
		backdropUrl,
		releaseDate,
		duration,
		Rating.CreateNew(),
		ageRestriction,
		trailerUrl,
		isActive);
}