using AutoMapper;
using SampleApp.Application.Features.Customers.Queries.Get;
using SampleApp.Application.Features.Customers.Queries.Search;
using SampleApp.Domain.Entities;

namespace SampleApp.Application.Features.Customers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateProjection<Customer, GetCustomerQueryResponse>();
        CreateProjection<Customer, SearchCustomersQueryResponse>();
    }
}
