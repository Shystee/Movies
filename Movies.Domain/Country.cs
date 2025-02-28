using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class Country : Entity<CountryId>, IAuditable
{
	private Country(
		CountryId id,
		string name,
		string isoCode)
		: base(id)
	{
		Name = name;
		IsoCode = isoCode;
		CreatedAt = DateTimeOffset.UtcNow;
		UpdatedAt = DateTimeOffset.UtcNow;
	}

	[UsedImplicitly]
	private Country()
	{
	}

	public DateTimeOffset CreatedAt { get; set; }

	public string IsoCode { get; private set; } = null!;

	public string Name { get; private set; } = null!;

	public DateTimeOffset UpdatedAt { get; set; }

	public static Country Create(string name, string isoCode) => new(
		CountryId.CreateUnique(),
		name,
		isoCode);
}