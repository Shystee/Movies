using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class UserWatchProgressId : EntityId<Guid>
{
	private UserWatchProgressId(Guid id)
		: base(id)
	{
	}

	[UsedImplicitly]
	private UserWatchProgressId()
	{
	}

	public static UserWatchProgressId CreateUnique() => new(Guid.NewGuid());
}