using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class VideoAsset : AggregateRoot<VideoAssetId, Guid>, IAuditable
{
    private readonly List<VideoStream> _streams = new();

    private VideoAsset(
        VideoAssetId id,
        Guid contentId,
        ContentType contentType,
        string title,
        string filePath,
        string storageBucket,
        string fileFormat,
        long fileSize,
        int durationSeconds)
        : base(id)
    {
        ContentId = contentId;
        ContentType = contentType;
        Title = title;
        FilePath = filePath;
        StorageBucket = storageBucket;
        FileFormat = fileFormat;
        FileSize = fileSize;
        DurationSeconds = durationSeconds;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    [UsedImplicitly]
    private VideoAsset()
    {
    }

    public Guid ContentId { get; private set; }
    public ContentType ContentType { get; private set; }
    public string Title { get; private set; } = null!;
    public string FilePath { get; private set; } = null!;
    public string StorageBucket { get; private set; } = null!;
    public string FileFormat { get; private set; } = null!;
    public long FileSize { get; private set; }
    public int DurationSeconds { get; private set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    // Navigation property
    public IReadOnlyList<VideoStream> Streams => _streams.AsReadOnly();

    public static ErrorOr<VideoAsset> Create(
        Guid contentId,
        ContentType contentType,
        string title,
        string filePath,
        string storageBucket,
        string fileFormat,
        long fileSize,
        int durationSeconds)
    {
        // Validate inputs
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(title))
        {
            errors.Add(DomainErrors.VideoAsset.Title.Empty);
        }
        else if (title.Length > 255)
        {
            errors.Add(DomainErrors.VideoAsset.Title.TooLong);
        }

        if (string.IsNullOrWhiteSpace(filePath))
        {
            errors.Add(DomainErrors.VideoAsset.FilePath.Empty);
        }
        else if (filePath.Length > 1024)
        {
            errors.Add(DomainErrors.VideoAsset.FilePath.TooLong);
        }

        if (string.IsNullOrWhiteSpace(storageBucket))
        {
            errors.Add(DomainErrors.VideoAsset.StorageBucket.Empty);
        }
        else if (storageBucket.Length > 255)
        {
            errors.Add(DomainErrors.VideoAsset.StorageBucket.TooLong);
        }

        if (string.IsNullOrWhiteSpace(fileFormat))
        {
            errors.Add(DomainErrors.VideoAsset.FileFormat.Empty);
        }

        if (fileSize <= 0)
        {
            errors.Add(DomainErrors.VideoAsset.FileSize.Invalid);
        }

        if (durationSeconds <= 0)
        {
            errors.Add(DomainErrors.VideoAsset.Duration.Invalid);
        }

        if (errors.Any())
        {
            return errors;
        }

        return new VideoAsset(
            VideoAssetId.CreateUnique(),
            contentId,
            contentType,
            title,
            filePath,
            storageBucket,
            fileFormat,
            fileSize,
            durationSeconds);
    }

    public void AddStream(VideoStream stream)
    {
        _streams.Add(stream);
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void RemoveStream(VideoStreamId streamId)
    {
        var stream = _streams.FirstOrDefault(s => s.Id == streamId);
        if (stream != null)
        {
            _streams.Remove(stream);
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }

    public void UpdateMetadata(
        string title,
        string filePath,
        string storageBucket,
        string fileFormat,
        long fileSize,
        int durationSeconds)
    {
        Title = title;
        FilePath = filePath;
        StorageBucket = storageBucket;
        FileFormat = fileFormat;
        FileSize = fileSize;
        DurationSeconds = durationSeconds;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}