using SampleApp.Domain.Common;
using SampleApp.Domain.Entities;

namespace SampleApp.Domain.Events.Customers;

public class CustomerCreatedEvent(Customer customer) : BaseEvent
{
    public Customer Customer { get; } = customer;
}
