using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class VideoStreamId : EntityId<Guid>
{
	private VideoStreamId(Guid id) : base(id) { }

	[UsedImplicitly]
	private VideoStreamId() { }

	public static ErrorOr<VideoStreamId> Create(Guid value)
	{
		if (value == Guid.Empty)
		{
			return DomainErrors.VideoStream.InvalidVideoStreamId;
		}

		return new VideoStreamId(value);
	}

	public static VideoStreamId CreateUnique() => new(Guid.NewGuid());
}