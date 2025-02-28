using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class LanguageId : EntityId<Guid>
{
	private LanguageId(Guid id) : base(id) { }

	[UsedImplicitly]
	private LanguageId() { }

	public static LanguageId CreateUnique() => new(Guid.NewGuid());
}