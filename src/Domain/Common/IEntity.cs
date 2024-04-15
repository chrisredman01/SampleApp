﻿namespace SampleApp.Domain.Common;

public interface IEntity
{
    public IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    public void AddDomainEvent(IDomainEvent domainEvent);
    public void ClearDomainEvents();
}
