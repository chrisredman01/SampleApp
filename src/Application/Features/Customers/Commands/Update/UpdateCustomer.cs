using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleApp.Application.Common.Interfaces;
using SampleApp.Application.Common.Models;
using SampleApp.Domain.Entities;

namespace SampleApp.Application.Features.Customers.Commands.Update;

public class UpdateCustomerCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Telephone { get; set; }
    public string? Email { get; set; }
}

public class UpdateCustomerCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<UpdateCustomerCommand, Result>
{
    public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await dbContext.Customers.FirstOrDefaultAsync(item => item.Id == request.Id);

        if (customer is null)
        {
            return Result.Failure<Customer>(CustomerErrors.NotFound(request.Id));
        }

        customer.Name = request.Name;
        customer.Telephone = request.Telephone;
        customer.Email = request.Email;

        if (await IsDuplicateAsync(customer.Id, customer.Name))
        {
            return Result.Failure<Customer>(CustomerErrors.IsExisting(customer.Name));
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private async Task<bool> IsDuplicateAsync(Guid id, string name) =>
        await dbContext.Customers
        .Where(item => item.Id != id)
        .Where(item => item.Name == name)
        .AnyAsync();
}
