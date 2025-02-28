using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class Title : ValueObject
{
	public const int MaxLength = 50;

	private Title(string value)
	{
		Value = value;
	}

	[UsedImplicitly]
	private Title()
	{
	}

	public string Value { get; } = null!;

	public static ErrorOr<Title> Create(string title)
	{
		return title.ToErrorOr()
			.FailIf(string.IsNullOrEmpty, DomainErrors.Movie.Title.Empty)
			.FailIf(val => val.Length > MaxLength, DomainErrors.Movie.Title.TooLong)
			.Then(val => new Title(val));
	}

	public override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Value;
	}
}