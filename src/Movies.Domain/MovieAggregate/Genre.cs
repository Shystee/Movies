using JetBrains.Annotations;
using Movies.Domain.Common;
using Movies.Domain.MovieAggregate.ValueObjects;

namespace Movies.Domain.MovieAggregate;

public sealed class Genre : Entity<GenreId, Guid>
{
	private Genre(
		GenreId id,
		GenreName name,
		Description description)
		: base(id)
	{
		Name = name;
		Description = description;
	}

	[UsedImplicitly]
	private Genre()
	{
	}

	public Description Description { get; private set; } = null!;

	public GenreName Name { get; private set; } = null!;

	public static Genre Create(GenreName name, Description description) => new(
		GenreId.CreateUnique(),
		name,
		description);
}