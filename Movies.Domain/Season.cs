using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class Season : Entity<SeasonId>, IAuditable
{
	private readonly List<Episode> _episodes = new();

	private Season(
		SeasonId id,
		TvShowId showId,
		int seasonNumber,
		Title title,
		Description description,
		Url posterUrl,
		ReleaseDate airDate,
		int episodeCount)
		: base(id)
	{
		ShowId = showId;
		SeasonNumber = seasonNumber;
		Title = title;
		Description = description;
		PosterUrl = posterUrl;
		AirDate = airDate;
		EpisodeCount = episodeCount;
		IsActive = true;
		CreatedAt = DateTimeOffset.Now;
		UpdatedAt = DateTimeOffset.Now;
	}

	[UsedImplicitly]
	private Season()
	{
	}

	public ReleaseDate AirDate { get; private set; } = null!;

	public DateTimeOffset CreatedAt { get; set; }

	public Description Description { get; private set; } = null!;

	public int EpisodeCount { get; private set; }

	public IReadOnlyList<Episode> Episodes => _episodes.AsReadOnly();

	public bool IsActive { get; private set; }

	public Url PosterUrl { get; private set; } = null!;

	public int SeasonNumber { get; private set; }

	public TvShowId ShowId { get; private set; } = null!;

	public Title Title { get; private set; } = null!;

	public DateTimeOffset UpdatedAt { get; set; }

	public static Season Create(
		TvShowId showId,
		int seasonNumber,
		Title title,
		Description description,
		Url posterUrl,
		ReleaseDate airDate,
		int episodeCount) => new(
		SeasonId.CreateUnique(),
		showId,
		seasonNumber,
		title,
		description,
		posterUrl,
		airDate,
		episodeCount);
}