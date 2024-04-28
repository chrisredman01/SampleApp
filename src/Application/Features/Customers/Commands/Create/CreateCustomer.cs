using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleApp.Application.Common.Interfaces;
using SampleApp.Application.Common.Models;
using SampleApp.Domain.Entities;

namespace SampleApp.Application.Features.Customers.Commands.Create;

public class CreateCustomerCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; } = string.Empty;
    public string? Telephone { get; set; }
    public string? Email { get; set; }
}

public class CreateCustomerCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<CreateCustomerCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = Customer.Create(
            request.Name,
            request.Telephone,
            request.Email
        );

        if (await IsDuplicateAsync(customer.Name))
        {
            return Result.Failure<Guid>(CustomerErrors.IsExisting(customer.Name));
        }

        dbContext.Customers.Add(customer);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(customer.Id);
    }

    private async Task<bool> IsDuplicateAsync(string name) =>
        await dbContext.Customers.AnyAsync(item => item.Name == name);
}
