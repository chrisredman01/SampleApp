using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleApp.Application.Common.Interfaces;
using SampleApp.Application.Common.Models;
namespace SampleApp.Application.Features.Customers.Queries.Get;

public record GetCustomerQuery(Guid Id) : IRequest<Result<GetCustomerQueryResponse>>;

public record GetCustomerQueryResponse(Guid Id, string Name, string? Telephone, string? Email, DateTime createdUtc, DateTime modifiedUtc);

public class GetCustomerQueryHandler(IApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<GetCustomerQuery, Result<GetCustomerQueryResponse>>
{
    public async Task<Result<GetCustomerQueryResponse>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await dbContext.Customers
            .Where(item => item.Id == request.Id)
            .ProjectTo<GetCustomerQueryResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (customer is null)
        {
            return Result.Failure<GetCustomerQueryResponse>(CustomerErrors.NotFound(request.Id));
        }

        return Result.Success(customer);
    }
}
