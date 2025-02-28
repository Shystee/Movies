namespace Movies.Domain.Common;

public record EntityId<TId> : ValueObject<TId> where TId : notnull
{
	protected EntityId(TId value)
		: base(value)
	{
	}
}