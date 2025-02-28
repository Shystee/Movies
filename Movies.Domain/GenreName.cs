using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class GenreName : ValueObject
{
	public const int MaxLength = 50;

	private GenreName(string value)
	{
		Value = value;
	}

	[UsedImplicitly]
	private GenreName()
	{
	}

	public string Value { get; } = null!;

	public static ErrorOr<GenreName> Create(string name)
	{
		return name.ToErrorOr()
			.FailIf(string.IsNullOrEmpty, DomainErrors.Genre.Name.Empty)
			.FailIf(val => val.Length > MaxLength, DomainErrors.Genre.Name.TooLong)
			.Then(val => new GenreName(val));
	}

	public override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Value;
	}
}