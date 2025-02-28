using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class UserWatchlist : Entity<UserWatchlistId>, IAuditable
{
	private UserWatchlist(
		UserWatchlistId id,
		Guid contentId,
		ContentType contentType,
		DateTimeOffset addedAt)
		: base(id)
	{
		ContentId = contentId;
		ContentType = contentType;
		AddedAt = addedAt;
		CreatedAt = DateTimeOffset.UtcNow;
		UpdatedAt = DateTimeOffset.UtcNow;
	}

	[UsedImplicitly]
	private UserWatchlist()
	{
	}

	public DateTimeOffset AddedAt { get; private set; }

	public Guid ContentId { get; private set; }

	public ContentType ContentType { get; private set; }

	public DateTimeOffset CreatedAt { get; set; }

	public DateTimeOffset UpdatedAt { get; set; }

	public static UserWatchlist Create(Guid contentId, ContentType contentType) => new(
		UserWatchlistId.CreateUnique(),
		contentId,
		contentType,
		DateTimeOffset.UtcNow);
}