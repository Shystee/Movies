using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class Tagline : ValueObject
{
	public const int MaxLength = 120;

	private Tagline(string value)
	{
		Value = value;
	}

	[UsedImplicitly]
	private Tagline()
	{
	}

	public string Value { get; } = null!;

	public static ErrorOr<Tagline> Create(string tagline)
	{
		return tagline.ToErrorOr()
			.FailIf(val => val?.Length > MaxLength, DomainErrors.Movie.Tagline.TooLong)
			.Then(val => new Tagline(val));
	}

	public override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Value;
	}
}