using ErrorOr;
using Movies.Domain.Common;

namespace Movies.Domain.MovieAggregate.ValueObjects;

public sealed record AgeRestriction : ValueObject<string>
{
	private static readonly HashSet<string> ValidRatings = new(StringComparer.OrdinalIgnoreCase)
	{
		// US ratings
		"G",
		"PG-13",
		"R",
		"NC-17",

		// UK ratings
		"U",
		"12",
		"12A",
		"15",
		"18",

		// Other common ratings
		"PG",
		"E",
		"E10+",
		"T",
		"M",
		"AO",
		"ALL",
	};

	private AgeRestriction(string value)
		: base(value)
	{
		Value = value;
	}

	public static ErrorOr<AgeRestriction> Create(string? restriction)
	{
		if (string.IsNullOrWhiteSpace(restriction))
		{
			return new AgeRestriction(string.Empty);
		}

		return restriction.ToErrorOr()
			.FailIf(val => !ValidRatings.Contains(val), DomainErrors.Movie.AgeRestriction.Invalid)
			.Then(val => new AgeRestriction(val));
	}
}