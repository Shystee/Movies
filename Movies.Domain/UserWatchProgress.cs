using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class UserWatchProgress : Entity<UserWatchProgressId>, IAuditable
{
	private UserWatchProgress(
		UserWatchProgressId id,
		Guid contentId,
		ContentType contentType,
		int positionSeconds,
		float percentageComplete)
		: base(id)
	{
		ContentId = contentId;
		ContentType = contentType;
		PositionSeconds = positionSeconds;
		PercentageComplete = percentageComplete;
		LastWatchedAt = DateTimeOffset.UtcNow;
		CreatedAt = DateTimeOffset.UtcNow;
		UpdatedAt = DateTimeOffset.UtcNow;
	}

	[UsedImplicitly]
	private UserWatchProgress()
	{
	}

	public Guid ContentId { get; private set; }

	public ContentType ContentType { get; private set; }

	public DateTimeOffset CreatedAt { get; set; }

	public DateTimeOffset LastWatchedAt { get; private set; }

	public float PercentageComplete { get; private set; }

	public int PositionSeconds { get; private set; }

	public DateTimeOffset UpdatedAt { get; set; }

	public static ErrorOr<UserWatchProgress> Create(
		Guid contentId,
		ContentType contentType,
		int positionSeconds,
		float percentageComplete)
	{
		// Validate position
		if (positionSeconds < 0)
		{
			return DomainErrors.User.WatchProgress.InvalidPosition;
		}

		// Validate percentage
		if (percentageComplete < 0 || percentageComplete > 100)
		{
			return DomainErrors.User.WatchProgress.InvalidPercentage;
		}

		return new UserWatchProgress(
			UserWatchProgressId.CreateUnique(),
			contentId,
			contentType,
			positionSeconds,
			percentageComplete);
	}

	public ErrorOr<Updated> Update(int positionSeconds, float percentageComplete)
	{
		// Validate position
		if (positionSeconds < 0)
		{
			return DomainErrors.User.WatchProgress.InvalidPosition;
		}

		// Validate percentage
		if (percentageComplete < 0 || percentageComplete > 100)
		{
			return DomainErrors.User.WatchProgress.InvalidPercentage;
		}

		PositionSeconds = positionSeconds;
		PercentageComplete = percentageComplete;
		LastWatchedAt = DateTimeOffset.UtcNow;
		UpdatedAt = DateTimeOffset.UtcNow;

		return Result.Updated;
	}
}