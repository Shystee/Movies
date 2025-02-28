using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class PersonName : ValueObject
{
	public const int MaxLength = 100;

	private PersonName(string value)
	{
		Value = value;
	}

	[UsedImplicitly]
	private PersonName()
	{
	}

	public string Value { get; } = null!;

	public static ErrorOr<PersonName> Create(string name)
	{
		return name.ToErrorOr()
			.FailIf(string.IsNullOrEmpty, DomainErrors.Person.Name.Empty)
			.FailIf(val => val.Length > MaxLength, DomainErrors.Person.Name.TooLong)
			.Then(val => new PersonName(val));
	}

	public override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Value;
	}
}