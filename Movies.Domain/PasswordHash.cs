using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class PasswordHash : ValueObject
{
	private PasswordHash(string value)
	{
		Value = value;
	}

	[UsedImplicitly]
	private PasswordHash()
	{
	}

	public string Value { get; } = null!;

	public static ErrorOr<PasswordHash> Create(string passwordHash)
	{
		if (string.IsNullOrEmpty(passwordHash))
		{
			return DomainErrors.PasswordHash.Empty;
		}

		return new PasswordHash(passwordHash);
	}

	public override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Value;
	}
}