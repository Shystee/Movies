using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class VideoAssetId : AggregateRootId<Guid>
{
	private VideoAssetId(Guid id)
		: base(id)
	{
	}

	[UsedImplicitly]
	private VideoAssetId()
	{
	}

	public static ErrorOr<VideoAssetId> Create(Guid value)
	{
		if (value == Guid.Empty)
		{
			return DomainErrors.VideoAsset.InvalidVideoAssetId;
		}

		return new VideoAssetId(value);
	}

	public static VideoAssetId CreateUnique() => new(Guid.NewGuid());
}