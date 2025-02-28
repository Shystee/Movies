using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class CountryId : EntityId<Guid>
{
    private CountryId(Guid id) : base(id) { }

    [UsedImplicitly]
    private CountryId() { }

    public static CountryId CreateUnique() => new(Guid.NewGuid());
}
