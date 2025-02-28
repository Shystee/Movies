using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class Description : ValueObject
{
	public const int MaxLength = 255;

	private Description(string value)
	{
		Value = value;
	}

	[UsedImplicitly]
	private Description()
	{
	}

	public string Value { get; } = null!;

	public static ErrorOr<Description> Create(string description)
	{
		return description.ToErrorOr()
			.FailIf(string.IsNullOrEmpty, DomainErrors.Movie.Description.Empty)
			.FailIf(val => val.Length > MaxLength, DomainErrors.Movie.Description.TooLong)
			.Then(val => new Description(val));
	}

	public override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Value;
	}
}