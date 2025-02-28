using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain.MovieAggregate.ValueObjects;

public sealed class MovieId : AggregateRootId<Guid>
{
	private MovieId(Guid id)
		: base(id)
	{
	}

	[UsedImplicitly]
	private MovieId()
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