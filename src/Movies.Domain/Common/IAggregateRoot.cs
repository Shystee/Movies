﻿namespace Movies.Domain.Common;

public interface IAggregateRoot
{
	IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
	
	void ClearDomainEvents();
}