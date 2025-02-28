using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class UserRatingId : EntityId<Guid>
{
	private UserRatingId(Guid id)
		: base(id)
	{
	}

	[UsedImplicitly]
	private UserRatingId()
	{
	}

	public static UserRatingId CreateUnique() => new(Guid.NewGuid());
}