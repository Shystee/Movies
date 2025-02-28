using JetBrains.Annotations;
using Movies.Domain.Common;
using Movies.Domain.MovieAggregate.ValueObjects;

namespace Movies.Domain.MovieAggregate;

public sealed class Movie : AggregateRoot<MovieId, Guid>
{
	private readonly List<Genre> _genres = [];

	private Movie(
		Title title,
		Tagline tagline,
		Description description,
		ReleaseDate releaseDate,
		Duration duration,
		AgeRestriction ageRestriction)
		: base(MovieId.CreateUnique())
	{
		Title = title;
		Tagline = tagline;
		Description = description;
		ReleaseDate = releaseDate;
		Duration = duration;
		AgeRestriction = ageRestriction;
	}

	private Movie(
		MovieId id,
		Title title,
		Tagline tagline,
		Description description,
		ReleaseDate releaseDate,
		Duration duration,
		AgeRestriction ageRestriction)
		: base(id)
	{
		Title = title;
		Tagline = tagline;
		Description = description;
		ReleaseDate = releaseDate;
		Duration = duration;
		AgeRestriction = ageRestriction;
	}

	[UsedImplicitly]
	private Movie()
	{
	}

	public AgeRestriction AgeRestriction { get; private set; } = null!;

	public Description Description { get; private set; } = null!;

	public Duration Duration { get; private set; } = null!;

	public IReadOnlyList<Genre> Genres => _genres.AsReadOnly();

	public Rating Rating { get; private set; } = null!;

	public ReleaseDate ReleaseDate { get; private set; } = null!;

	public Tagline Tagline { get; private set; } = null!;

	public Title Title { get; private set; } = null!;

	public static Movie Create(
		Title title,
		Tagline tagline,
		Description description,
		ReleaseDate releaseDate,
		Duration duration,
		AgeRestriction ageRestriction) => new(
		title,
		tagline,
		description,
		releaseDate,
		duration,
		ageRestriction);
}