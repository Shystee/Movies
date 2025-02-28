using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class UserName : ValueObject
{
	public const int MaxLength = 100;

	private UserName(string value)
	{
		Value = value;
	}

	[UsedImplicitly]
	private UserName()
	{
	}

	public string Value { get; } = null!;

	public static ErrorOr<UserName> Create(string name)
	{
		return name.ToErrorOr()
			.FailIf(string.IsNullOrWhiteSpace, DomainErrors.User.Name.Empty)
			.FailIf(val => val.Length > MaxLength, DomainErrors.User.Name.TooLong)
			.Then(val => new UserName(val));
	}

	public override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Value;
	}
}