using AutoMapper;
using SampleApp.API.Models.Requests;
using SampleApp.API.Models.Responses;
using SampleApp.Application.Common.Models;
using SampleApp.Application.Features.Customers.Commands.Create;
using SampleApp.Application.Features.Customers.Commands.Update;
using SampleApp.Application.Features.Customers.Queries.Get;
using SampleApp.Application.Features.Customers.Queries.Search;

namespace SampleApp.API;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<BaseSearchRequest, SearchQuery>();
        CreateMap<SearchCustomersRequest, SearchCustomersQuery>();
        CreateMap<CreateCustomerRequest, CreateCustomerCommand>();
        CreateMap<UpdateCustomerRequest, UpdateCustomerCommand>();
        CreateMap<GetCustomerQueryResponse, CustomerResponse>();
        CreateMap<SearchCustomersQueryResponse, SearchCustomerResponse>();
        CreateMap(typeof(PagedResults<>), typeof(PagedResultsResponse<>));
    }
}
