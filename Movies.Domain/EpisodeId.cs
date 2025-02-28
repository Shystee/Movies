using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class EpisodeId : EntityId<Guid>
{
	private EpisodeId(Guid id)
		: base(id)
	{
	}

	[UsedImplicitly]
	private EpisodeId()
	{
	}

	public static ErrorOr<EpisodeId> Create(Guid value)
	{
		if (value == Guid.Empty)
		{
			return DomainErrors.Episode.InvalidEpisodeId;
		}

		return new EpisodeId(value);
	}

	public static EpisodeId CreateUnique() => new(Guid.NewGuid());
}