using ErrorOr;
using JetBrains.Annotations;
using System.Text.RegularExpressions;

namespace Movies.Domain;

public sealed partial class Email : ValueObject
{
	public const int MaxLength = 320;
	private static readonly Regex EmailRegex = EmailRegexPattern();

	private Email(string value)
	{
		Value = value;
	}

	[UsedImplicitly]
	private Email()
	{
	}

	public string Value { get; } = null!;

	public static ErrorOr<Email> Create(string email)
	{
		return email.ToErrorOr()
			.FailIf(string.IsNullOrEmpty, DomainErrors.User.Email.Empty)
			.FailIf(val => val.Length > MaxLength, DomainErrors.User.Email.TooLong)
			.FailIf(val => !EmailRegex.IsMatch(val), DomainErrors.User.Email.Invalid)
			.Then(val => new Email(val));
	}

	public override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Value.ToLowerInvariant(); // Case-insensitive comparison for emails
	}

	[GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled)]
	private static partial Regex EmailRegexPattern();
}