using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class TvShow : AggregateRoot<TvShowId, Guid>, IAuditable
{
	private readonly List<Season> _seasons = [];

	private TvShow(
		TvShowId id,
		Title title,
		Tagline tagline,
		Description description,
		Url posterUrl,
		Url backdropUrl,
		ReleaseDate firstAirDate,
		ReleaseDate? lastAirDate,
		int totalSeasons,
		int totalEpisodes,
		Rating rating,
		AgeRestriction ageRestriction,
		ShowStatus status,
		Url trailerUrl,
		bool isActive)
		: base(id)
	{
		Title = title;
		Tagline = tagline;
		Description = description;
		PosterUrl = posterUrl;
		BackdropUrl = backdropUrl;
		FirstAirDate = firstAirDate;
		LastAirDate = lastAirDate;
		TotalSeasons = totalSeasons;
		TotalEpisodes = totalEpisodes;
		Rating = rating;
		AgeRestriction = ageRestriction;
		Status = status;
		TrailerUrl = trailerUrl;
		IsActive = isActive;
		CreatedAt = DateTimeOffset.Now;
		UpdatedAt = DateTimeOffset.Now;
	}

	[UsedImplicitly]
	private TvShow()
	{
	}

	public AgeRestriction AgeRestriction { get; private set; } = null!;

	public Url BackdropUrl { get; private set; } = null!;

	public DateTimeOffset CreatedAt { get; set; }

	public Description Description { get; private set; } = null!;

	public ReleaseDate FirstAirDate { get; private set; } = null!;

	public bool IsActive { get; private set; }

	public ReleaseDate? LastAirDate { get; private set; }

	public Url PosterUrl { get; private set; } = null!;

	public Rating Rating { get; private set; } = null!;

	public IReadOnlyList<Season> Seasons => _seasons.AsReadOnly();

	public ShowStatus Status { get; private set; }

	public Tagline Tagline { get; private set; } = null!;

	public Title Title { get; private set; } = null!;

	public int TotalEpisodes { get; private set; }

	public int TotalSeasons { get; private set; }

	public Url TrailerUrl { get; private set; } = null!;

	public DateTimeOffset UpdatedAt { get; set; }

	public static TvShow Create(
		Title title,
		Tagline tagline,
		Description description,
		Url posterUrl,
		Url backdropUrl,
		ReleaseDate firstAirDate,
		ReleaseDate? lastAirDate,
		AgeRestriction ageRestriction,
		ShowStatus status,
		Url trailerUrl,
		bool isActive = true) => new(
		TvShowId.CreateUnique(),
		title,
		tagline,
		description,
		posterUrl,
		backdropUrl,
		firstAirDate,
		lastAirDate,
		0, // Initial seasons count
		0, // Initial episodes count
		Rating.CreateNew(),
		ageRestriction,
		status,
		trailerUrl,
		isActive);
}