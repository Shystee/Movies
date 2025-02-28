using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class UserPreferences : Entity<UserPreferencesId>, IAuditable
{
	private UserPreferences(
		UserPreferencesId id,
		string? subtitleLanguage,
		string? audioLanguage,
		StreamingQuality streamingQuality,
		bool autoplayNext,
		bool autoplayPreviews)
		: base(id)
	{
		SubtitleLanguage = subtitleLanguage;
		AudioLanguage = audioLanguage;
		StreamingQuality = streamingQuality;
		AutoplayNext = autoplayNext;
		AutoplayPreviews = autoplayPreviews;
		CreatedAt = DateTimeOffset.UtcNow;
		UpdatedAt = DateTimeOffset.UtcNow;
	}

	[UsedImplicitly]
	private UserPreferences()
	{
	}

	public string? AudioLanguage { get; private set; }

	public bool AutoplayNext { get; private set; }

	public bool AutoplayPreviews { get; private set; }

	public DateTimeOffset CreatedAt { get; set; }

	public StreamingQuality StreamingQuality { get; private set; }

	public string? SubtitleLanguage { get; private set; }

	public DateTimeOffset UpdatedAt { get; set; }

	public static UserPreferences Create(
		string? subtitleLanguage = null,
		string? audioLanguage = null,
		StreamingQuality streamingQuality = StreamingQuality.Auto,
		bool autoplayNext = true,
		bool autoplayPreviews = true) => new(
		UserPreferencesId.CreateUnique(),
		subtitleLanguage,
		audioLanguage,
		streamingQuality,
		autoplayNext,
		autoplayPreviews);

	public void UpdateAudioLanguage(string? language)
	{
		AudioLanguage = language;
		UpdatedAt = DateTimeOffset.UtcNow;
	}

	public void UpdateAutoplaySettings(bool autoplayNext, bool autoplayPreviews)
	{
		AutoplayNext = autoplayNext;
		AutoplayPreviews = autoplayPreviews;
		UpdatedAt = DateTimeOffset.UtcNow;
	}

	public void UpdateStreamingQuality(StreamingQuality quality)
	{
		StreamingQuality = quality;
		UpdatedAt = DateTimeOffset.UtcNow;
	}

	public void UpdateSubtitleLanguage(string? language)
	{
		SubtitleLanguage = language;
		UpdatedAt = DateTimeOffset.UtcNow;
	}
}