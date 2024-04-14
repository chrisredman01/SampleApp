namespace SampleApp.Domain.Common;

public interface IEntity
{
    public IReadOnlyCollection<BaseEvent> DomainEvents { get; }
    public void AddDomainEvent(BaseEvent domainEvent);
    public void ClearDomainEvents();
}
