namespace Movies.Domain;

public abstract class AggregateRootId<TId> : EntityId<TId>
{
	protected AggregateRootId(TId value)
		: base(value)
	{
	}

	protected AggregateRootId()
	{
	}
}