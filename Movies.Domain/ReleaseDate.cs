using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class ReleaseDate : ValueObject
{
	private static readonly DateOnly MaxDate = DateOnly.MaxValue;
	private static readonly DateOnly MinDate = new(1888, 1, 1);

	private ReleaseDate(DateOnly date)
	{
		Value = date;
	}

	[UsedImplicitly]
	private ReleaseDate()
	{
	}

	public DateOnly Value { get; }

	public static ErrorOr<ReleaseDate> Create(DateOnly date)
	{
		return date.ToErrorOr()
			.FailIf(val => val < MinDate || val > MaxDate, DomainErrors.Movie.ReleaseDate.Invalid)
			.Then(val => new ReleaseDate(val));
	}

	public static ErrorOr<ReleaseDate> Create(DateTime dateTime) => Create(DateOnly.FromDateTime(dateTime));

	public override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Value;
	}
}