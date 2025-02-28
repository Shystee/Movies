namespace Movies.Domain.Common;

public abstract class AggregateRoot<TId, TIdType> : Entity<TId, TIdType>, IAggregateRoot
	where TId : EntityId<TIdType> where TIdType : notnull
{
	private readonly List<IDomainEvent> _domainEvents = [];

	protected AggregateRoot(TId id)
		: base(id)
	{
	}

	protected AggregateRoot()
	{
	}

	public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

	public void ClearDomainEvents()
	{
		_domainEvents.Clear();
	}

	protected void AddDomainEvent(IDomainEvent domainEvent)
	{
		_domainEvents.Add(domainEvent);
	}
}