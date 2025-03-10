﻿namespace Movies.Domain;

public abstract class AggregateRoot<TId, TIdType> : Entity<TId> where TId : AggregateRootId<TIdType>
{
	protected AggregateRoot(TId id)
	{
		Id = id;
	}
	
	protected AggregateRoot()
	{
	}

	public new AggregateRootId<TIdType> Id { get; } = null!;
}