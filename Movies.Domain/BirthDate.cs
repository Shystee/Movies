using ErrorOr;
using JetBrains.Annotations;

namespace Movies.Domain;

public sealed class BirthDate : ValueObject
{
	private static readonly DateOnly MaxDate = DateOnly.FromDateTime(DateTime.UtcNow);
	private static readonly DateOnly MinDate = new(1850, 1, 1);

	private BirthDate(DateOnly? date)
	{
		Value = date;
	}

	[UsedImplicitly]
	private BirthDate()
	{
	}

	public DateOnly? Value { get; }

	public static ErrorOr<BirthDate> Create(DateOnly? date)
	{
		if (date == null)
		{
			return new BirthDate(null);
		}

		if (date < MinDate || date > MaxDate)
		{
			return DomainErrors.Person.BirthDate.Invalid;
		}

		return new BirthDate(date);
	}

	public static ErrorOr<BirthDate> Create(DateTime? dateTime)
	{
		if (dateTime == null)
		{
			return new BirthDate(null);
		}

		return Create(DateOnly.FromDateTime(dateTime.Value));
	}

	public override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Value;
	}
}