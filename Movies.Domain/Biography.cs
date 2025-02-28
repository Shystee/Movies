using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class Biography : ValueObject
{
	public const int MaxLength = 2000;

	private Biography(string value)
	{
		Value = value;
	}

	[UsedImplicitly]
	private Biography()
	{
	}

	public string Value { get; } = null!;

	public static ErrorOr<Biography> Create(string biography)
	{
		return biography.ToErrorOr()
			.FailIf(string.IsNullOrEmpty, DomainErrors.Person.Biography.Empty)
			.FailIf(val => val.Length > MaxLength, DomainErrors.Person.Biography.TooLong)
			.Then(val => new Biography(val));
	}

	public override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Value;
	}
}