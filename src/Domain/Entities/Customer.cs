using SampleApp.Domain.Events.Customers;
using SampleApp.Domain.Common;

namespace SampleApp.Domain.Entities;

public class Customer : Entity
{
    public string Name { get; set; } = string.Empty;
    public string? Telephone { get; set; }
    public string? Email { get; set; }

    public static Customer Create(string name, string? telephone, string? email)
    {
        var entity = new Customer
        {
            Name = name,
            Telephone = telephone,
            Email = email
        };

        entity.AddDomainEvent(new CustomerCreatedEvent(entity));

        return entity;
    }
}
