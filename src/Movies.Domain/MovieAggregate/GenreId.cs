using ErrorOr;
using Movies.Domain.Common;

namespace Movies.Domain.MovieAggregate;

public sealed record GenreId : EntityId<Guid>
{
	private GenreId(Guid id)
		: base(id)
	{
	}

	public static ErrorOr<GenreId> Create(Guid value)
	{
		if (value == Guid.Empty)
		{
			return DomainErrors.Genre.InvalidGenreId;
		}

		return new GenreId(value);
	}

	public static GenreId CreateUnique() => new(Guid.NewGuid());
}