using ErrorOr;
using Movies.Domain.Common;

namespace Movies.Domain.MovieAggregate.ValueObjects;

public sealed record MovieId : EntityId<Guid>
{
	private MovieId(Guid id)
		: base(id)
	{
	}

	public static ErrorOr<MovieId> Create(Guid value)
	{
		if (value == Guid.Empty)
		{
			return DomainErrors.Movie.InvalidMovieId;
		}

		return new MovieId(value);
	}

	public static MovieId CreateUnique() => new(Guid.NewGuid());
}