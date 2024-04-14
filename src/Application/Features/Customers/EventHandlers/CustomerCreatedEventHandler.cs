using MediatR;
using Microsoft.Extensions.Logging;
using SampleApp.Domain.Events.Customers;

namespace SampleApp.Application.Features.Customers.EventHandlers;

public class CustomerCreatedEventHandler(ILogger<CustomerCreatedEventHandler> logger) : INotificationHandler<CustomerCreatedEvent>
{
    public Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Customer '{Name}' created", notification.Customer.Name);

        return Task.CompletedTask;
    }
}
