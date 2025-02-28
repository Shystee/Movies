namespace Movies.Domain.Common;

public abstract class Entity<TId, TIdType> where TId : EntityId<TIdType> where TIdType : notnull
{
	protected Entity(TId id)
	{
		Id = id;
	}

	protected Entity()
	{
	}

	public TId Id { get; } = null!;

	public static bool operator ==(Entity<TId, TIdType>? left, Entity<TId, TIdType>? right) => Equals(left, right);

	public static bool operator !=(Entity<TId, TIdType>? left, Entity<TId, TIdType>? right) => !Equals(left, right);

	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
		return Equals((Entity<TId, TIdType>)obj);
	}

	public bool Equals(Entity<TId, TIdType>? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return EqualityComparer<TId>.Default.Equals(Id, other.Id);
	}

	public override int GetHashCode() => EqualityComparer<TId>.Default.GetHashCode(Id);
}