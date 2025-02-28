namespace Movies.Domain;

public class EntityId<TId> : ValueObject
{
	protected EntityId(TId value)
	{
		Value = value;
	}

	protected EntityId()
	{
	}

	public TId Value { get; } = default!;

	public override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Value;
	}

	public override string? ToString() => Value?.ToString() ?? base.ToString();
}