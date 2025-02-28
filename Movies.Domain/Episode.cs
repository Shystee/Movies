using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class Episode : Entity<EpisodeId>, IAuditable
{
	private Episode(
		EpisodeId id,
		SeasonId seasonId,
		int episodeNumber,
		string title,
		string overview,
		DateOnly airDate,
		int durationMinutes,
		string stillUrl)
		: base(id)
	{
		SeasonId = seasonId;
		EpisodeNumber = episodeNumber;
		Title = title;
		Overview = overview;
		AirDate = airDate;
		DurationMinutes = durationMinutes;
		StillUrl = stillUrl;
		IsActive = true;
		CreatedAt = DateTimeOffset.Now;
		UpdatedAt = DateTimeOffset.Now;
	}

	[UsedImplicitly]
	private Episode()
	{
	}

	public DateOnly AirDate { get; private set; }

	public DateTimeOffset CreatedAt { get; set; }

	public int DurationMinutes { get; private set; }

	public int EpisodeNumber { get; private set; }

	public bool IsActive { get; private set; }

	public string Overview { get; private set; } = null!;

	public SeasonId SeasonId { get; private set; } = null!;

	public string StillUrl { get; private set; } = null!;

	public string Title { get; private set; } = null!;

	public DateTimeOffset UpdatedAt { get; set; }

	public static Episode Create(
		SeasonId seasonId,
		int episodeNumber,
		string title,
		string overview,
		DateOnly airDate,
		int durationMinutes,
		string stillUrl) => new(
		EpisodeId.CreateUnique(),
		seasonId,
		episodeNumber,
		title,
		overview,
		airDate,
		durationMinutes,
		stillUrl);
}