using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class VideoStream : Entity<VideoStreamId>, IAuditable
{
	private VideoStream(
		VideoStreamId id,
		VideoQuality quality,
		VideoFormat format,
		string manifestUrl,
		string? encryptionKey)
		: base(id)
	{
		Quality = quality;
		Format = format;
		ManifestUrl = manifestUrl;
		EncryptionKey = encryptionKey;
		CreatedAt = DateTimeOffset.UtcNow;
		UpdatedAt = DateTimeOffset.UtcNow;
	}

	[UsedImplicitly]
	private VideoStream()
	{
	}

	public DateTimeOffset CreatedAt { get; set; }

	public string? EncryptionKey { get; private set; }

	public VideoFormat Format { get; private set; }

	public string ManifestUrl { get; private set; } = null!;

	public VideoQuality Quality { get; private set; }

	public DateTimeOffset UpdatedAt { get; set; }

	public static ErrorOr<VideoStream> Create(
		VideoQuality quality,
		VideoFormat format,
		string manifestUrl,
		string? encryptionKey = null)
	{
		// Validate manifest URL
		if (string.IsNullOrWhiteSpace(manifestUrl))
		{
			return DomainErrors.VideoStream.ManifestUrl.Empty;
		}

		// Create a simple URL validation (could be enhanced with regex)
		if (!manifestUrl.StartsWith("http://") && !manifestUrl.StartsWith("https://"))
		{
			return DomainErrors.VideoStream.ManifestUrl.Invalid;
		}

		return new VideoStream(
			VideoStreamId.CreateUnique(),
			quality,
			format,
			manifestUrl,
			encryptionKey);
	}

	public void UpdateEncryptionKey(string? encryptionKey)
	{
		EncryptionKey = encryptionKey;
		UpdatedAt = DateTimeOffset.UtcNow;
	}

	public ErrorOr<Updated> UpdateManifestUrl(string manifestUrl)
	{
		if (string.IsNullOrWhiteSpace(manifestUrl))
		{
			return DomainErrors.VideoStream.ManifestUrl.Empty;
		}

		if (!manifestUrl.StartsWith("http://") && !manifestUrl.StartsWith("https://"))
		{
			return DomainErrors.VideoStream.ManifestUrl.Invalid;
		}

		ManifestUrl = manifestUrl;
		UpdatedAt = DateTimeOffset.UtcNow;
		return Result.Updated;
	}
}