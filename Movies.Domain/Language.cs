using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class Language : Entity<LanguageId>, IAuditable
{
	private Language(
		LanguageId id,
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
	private Language() { }

	public string Name { get; private set; } = null!;
	public string IsoCode { get; private set; } = null!;
	public DateTimeOffset CreatedAt { get; set; }
	public DateTimeOffset UpdatedAt { get; set; }

	public static Language Create(string name, string isoCode)
	{
		return new Language(
			LanguageId.CreateUnique(),
			name,
			isoCode);
	}
}