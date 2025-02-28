using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class UserId : AggregateRootId<Guid>
{
	private UserId(Guid id)
		: base(id)
	{
	}

	[UsedImplicitly]
	private UserId()
	{
	}

	public static ErrorOr<UserId> Create(Guid value)
	{
		if (value == Guid.Empty)
		{
			return DomainErrors.User.InvalidUserId;
		}

		return new UserId(value);
	}

	public static UserId CreateUnique() => new(Guid.NewGuid());
}