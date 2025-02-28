namespace Movies.Domain;

public abstract class Entity<TId> : IEquatable<Entity<TId>>, IHasDomainEvents
	where TId : ValueObject
{
	private readonly List<IDomainEvent> _domainEvents = [];

	protected Entity(TId id)
	{
		Id = id;
	}
	
	protected Entity()
	{
	}

	public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

	public TId Id { get; } = null!;

	public static bool operator ==(Entity<TId>? left, Entity<TId>? right) => Equals(left, right);

	public static bool operator !=(Entity<TId>? left, Entity<TId>? right) => !Equals(left, right);

	public void ClearDomainEvents() => _domainEvents.Clear();

	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
		return Equals((Entity<TId>)obj);
	}

	public bool Equals(Entity<TId>? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return EqualityComparer<TId>.Default.Equals(Id, other.Id);
	}

	public override int GetHashCode() => EqualityComparer<TId>.Default.GetHashCode(Id);

	protected void AddDomainEvent(IDomainEvent @event) => _domainEvents.Add(@event);
}