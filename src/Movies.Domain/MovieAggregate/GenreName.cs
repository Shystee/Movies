using ErrorOr;
using Movies.Domain.Common;

namespace Movies.Domain.MovieAggregate;

public sealed record GenreName : ValueObject<string>
{
	public const int MaxLength = 50;

	private GenreName(string value)
		: base(value)
	{
	}

	public static ErrorOr<GenreName> Create(string name)
	{
		return name.ToErrorOr()
			.FailIf(string.IsNullOrEmpty, DomainErrors.Genre.Name.Empty)
			.FailIf(val => val.Length > MaxLength, DomainErrors.Genre.Name.TooLong)
			.Then(val => new GenreName(val));
	}
}