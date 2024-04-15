using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using SampleApp.Application.Common.Extensions;
using SampleApp.Application.Common.Interfaces;
using SampleApp.Application.Common.Models;
using SampleApp.Domain.Entities;
using System.Linq.Expressions;

namespace SampleApp.Application.Features.Customers.Queries.Search;

public class SearchCustomersQuery : SearchQuery, IRequest<Result<PagedResults<SearchCustomersQueryResponse>>>
{
    public string? SearchTerm { get; set; }
}

public record SearchCustomersQueryResponse(Guid Id, string Name, string? Telephone, string? Email, DateTime createdUtc, DateTime modifiedUtc);

public class SearchCustomersQueryHandler(IApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<SearchCustomersQuery, Result<PagedResults<SearchCustomersQueryResponse>>>
{
    public async Task<Result<PagedResults<SearchCustomersQueryResponse>>> Handle(SearchCustomersQuery request, CancellationToken cancellationToken)
    {
        var results = await dbContext.Customers
            .Where(ApplySearchFilter(request))
            .ProjectTo<SearchCustomersQueryResponse>(mapper.ConfigurationProvider)
            .AsPagedResultsAsync(request.Page, request.PageSize);

        return Result.Success(results);
    }

    private static Expression<Func<Customer, bool>> ApplySearchFilter(SearchCustomersQuery request) => item =>
        string.IsNullOrEmpty(request.SearchTerm) ||
        item.Name.Contains(request.SearchTerm) ||
        !string.IsNullOrEmpty(item.Email) && item.Email.Contains(request.SearchTerm);
}

