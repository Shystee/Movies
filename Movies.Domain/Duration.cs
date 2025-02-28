using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class Duration : ValueObject
{
	public const int MaxMinutes = 600;
	public const int MinMinutes = 1;

	private Duration(int minutes)
	{
		Minutes = minutes;
	}

	[UsedImplicitly]
	private Duration()
	{
	}

	public int Minutes { get; }

	public static ErrorOr<Duration> Create(int minutes)
	{
		return minutes.ToErrorOr()
			.FailIf(val => val is < MinMinutes or > MaxMinutes, DomainErrors.Movie.Duration.Invalid)
			.Then(val => new Duration(val));
	}

	public override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Minutes;
	}
}