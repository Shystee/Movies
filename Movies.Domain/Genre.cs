using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class Genre : Entity<GenreId>, IAuditable
{
	private Genre(
		GenreId id,
		GenreName name,
		Description description)
		: base(id)
	{
		Name = name;
		Description = description;
		CreatedAt = DateTimeOffset.UtcNow;
		UpdatedAt = DateTimeOffset.UtcNow;
	}

	[UsedImplicitly]
	private Genre()
	{
	}

	public DateTimeOffset CreatedAt { get; set; }

	public Description Description { get; private set; } = null!;

	public GenreName Name { get; private set; } = null!;

	public DateTimeOffset UpdatedAt { get; set; }

	public static Genre Create(GenreName name, Description description) => new(
		GenreId.CreateUnique(),
		name,
		description);
}