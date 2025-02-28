using ErrorOr;

namespace Movies.Domain;

public sealed class GenreId : EntityId<Guid>
{
	private GenreId(Guid id)
		: base(id)
	{
	}

	private GenreId()
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