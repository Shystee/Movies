using ErrorOr;
using JetBrains.Annotations;
using System.Text.RegularExpressions;

namespace Movies.Domain;

public sealed partial class Url : ValueObject
{
	public const int MaxLength = 500;

	private static readonly Regex UrlRegex = UrlRegexPattern();

	private Url(string value)
	{
		Value = value;
	}

	[UsedImplicitly]
	private Url()
	{
	}

	public string Value { get; } = null!;

	public static ErrorOr<Url> Create(string? url)
	{
		if (string.IsNullOrWhiteSpace(url))
		{
			return new Url(string.Empty);
		}

		return url.ToErrorOr()
			.FailIf(val => val.Length > MaxLength, DomainErrors.Url.TooLong)
			.FailIf(val => !UrlRegex.IsMatch(val), DomainErrors.Url.Invalid)
			.Then(val => new Url(val));
	}

	public override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Value;
	}

	[GeneratedRegex(@"^(https?):\/\/[^\s/$.?#].[^\s]*$", RegexOptions.IgnoreCase | RegexOptions.Compiled)]
	private static partial Regex UrlRegexPattern();
}