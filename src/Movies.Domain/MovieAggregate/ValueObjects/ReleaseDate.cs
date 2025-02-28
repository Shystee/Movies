using ErrorOr;
using Movies.Domain.Common;

namespace Movies.Domain.MovieAggregate.ValueObjects;

public sealed record ReleaseDate : ValueObject<DateOnly>
{
	private static readonly DateOnly MaxDate = DateOnly.MaxValue;
	private static readonly DateOnly MinDate = new(1888, 1, 1);

	private ReleaseDate(DateOnly value)
		: base(value)
	{
	}

	public static ErrorOr<ReleaseDate> Create(DateOnly date)
	{
		return date.ToErrorOr()
			.FailIf(val => val < MinDate || val > MaxDate, DomainErrors.Movie.ReleaseDate.Invalid)
			.Then(val => new ReleaseDate(val));
	}

	public static ErrorOr<ReleaseDate> Create(DateTime dateTime) => Create(DateOnly.FromDateTime(dateTime));
}