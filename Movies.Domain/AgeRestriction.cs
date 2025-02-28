using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class AgeRestriction : ValueObject
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
	{
		Value = value;
	}

	[UsedImplicitly]
	private AgeRestriction()
	{
	}

	public string Value { get; } = null!;

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

	public override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Value;
	}
}