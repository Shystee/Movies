using ErrorOr;
using Movies.Domain.Common;

namespace Movies.Domain.MovieAggregate.ValueObjects;

public sealed record Tagline : ValueObject<string>
{
	public const int MaxLength = 120;

	private Tagline(string value)
		: base(value)
	{
	}

	public static ErrorOr<Tagline> Create(string tagline)
	{
		return tagline.ToErrorOr()
			.FailIf(val => val?.Length > MaxLength, DomainErrors.Movie.Tagline.TooLong)
			.Then(val => new Tagline(val));
	}
}