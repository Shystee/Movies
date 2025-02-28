using ErrorOr;
using Movies.Domain.Common;

namespace Movies.Domain.MovieAggregate.ValueObjects;

public sealed record Description : ValueObject<string>
{
	public const int MaxLength = 255;

	private Description(string value)
		: base(value)
	{
	}

	public static ErrorOr<Description> Create(string description)
	{
		return description.ToErrorOr()
			.FailIf(string.IsNullOrEmpty, DomainErrors.Movie.Description.Empty)
			.FailIf(val => val.Length > MaxLength, DomainErrors.Movie.Description.TooLong)
			.Then(val => new Description(val));
	}
}