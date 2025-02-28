using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class UserWatchlistId : EntityId<Guid>
{
	private UserWatchlistId(Guid id)
		: base(id)
	{
	}

	[UsedImplicitly]
	private UserWatchlistId()
	{
	}

	public static UserWatchlistId CreateUnique() => new(Guid.NewGuid());
}