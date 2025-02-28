using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class UserPreferencesId : EntityId<Guid>
{
	private UserPreferencesId(Guid id)
		: base(id)
	{
	}

	[UsedImplicitly]
	private UserPreferencesId()
	{
	}

	public static UserPreferencesId CreateUnique() => new(Guid.NewGuid());
}