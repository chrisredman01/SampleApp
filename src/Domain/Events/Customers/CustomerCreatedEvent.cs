using SampleApp.Domain.Common;
using SampleApp.Domain.Entities;

namespace SampleApp.Domain.Events.Customers;

public record CustomerCreatedEvent(Customer Customer) : IDomainEvent;
