using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class SeasonId : EntityId<Guid>
{
	private SeasonId(Guid id)
		: base(id)
	{
	}

	[UsedImplicitly]
	private SeasonId()
	{
	}

	public static ErrorOr<SeasonId> Create(Guid value)
	{
		if (value == Guid.Empty)
		{
			return DomainErrors.Season.InvalidSeasonId;
		}

		return new SeasonId(value);
	}

	public static SeasonId CreateUnique() => new(Guid.NewGuid());
}