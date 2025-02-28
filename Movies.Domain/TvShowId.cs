using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class TvShowId : AggregateRootId<Guid>
{
	private TvShowId(Guid id)
		: base(id)
	{
	}

	[UsedImplicitly]
	private TvShowId()
	{
	}

	public static ErrorOr<TvShowId> Create(Guid value)
	{
		if (value == Guid.Empty)
		{
			return DomainErrors.TvShow.InvalidTvShowId;
		}

		return new TvShowId(value);
	}

	public static TvShowId CreateUnique() => new(Guid.NewGuid());
}