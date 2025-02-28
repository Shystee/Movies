using ErrorOr;
using Movies.Domain.Common;

namespace Movies.Domain.MovieAggregate.ValueObjects;

public sealed record Duration : ValueObject<int>
{
	public const int MaxMinutes = 600;
	public const int MinMinutes = 1;

	private Duration(int value)
		: base(value)
	{
	}

	public static ErrorOr<Duration> Create(int minutes)
	{
		return minutes.ToErrorOr()
			.FailIf(val => val is < MinMinutes or > MaxMinutes, DomainErrors.Movie.Duration.Invalid)
			.Then(val => new Duration(val));
	}
}