using ErrorOr;
using Movies.Domain.Common;

namespace Movies.Domain.MovieAggregate.ValueObjects;

public sealed record Title : ValueObject<string>
{
	public const int MaxLength = 50;

	private Title(string value)
		: base(value)
	{
	}

	public static ErrorOr<Title> Create(string title)
	{
		return title.ToErrorOr()
			.FailIf(string.IsNullOrEmpty, DomainErrors.Movie.Title.Empty)
			.FailIf(val => val.Length > MaxLength, DomainErrors.Movie.Title.TooLong)
			.Then(val => new Title(val));
	}
}