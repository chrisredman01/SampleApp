using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleApp.Application.Common.Interfaces;
using SampleApp.Application.Common.Models;

namespace SampleApp.Application.Features.Customers.Commands.Delete;

public record DeleteCustomerCommand(Guid Id) : IRequest<Result>;

public class DeleteCustomerCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<DeleteCustomerCommand, Result>
{
    public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var existingCustomer = await dbContext.Customers.FirstOrDefaultAsync(item => item.Id == request.Id);

        if (existingCustomer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(request.Id));
        }

        dbContext.Customers.Remove(existingCustomer);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
