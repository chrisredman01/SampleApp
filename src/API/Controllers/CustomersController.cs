using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Application.Features.Customers.Commands.Delete;
using SampleApp.API.Extensions;
using SampleApp.Application.Common.Extensions;
using SampleApp.Application.Common.Models;
using SampleApp.Application.Features.Customers.Commands.Create;
using SampleApp.Application.Features.Customers.Commands.Update;
using SampleApp.Application.Features.Customers.Queries.Get;
using SampleApp.Application.Features.Customers.Queries.Search;
using SampleApp.API.Models.Requests;
using AutoMapper;
using SampleApp.API.Models.Responses;

namespace SampleApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(CreateCustomerRequest request)
    {
        var command = mapper.Map<CreateCustomerCommand>(request);

        var result = await mediator.Send(command);

        return result.Match(
            onSuccess: () => Ok(result.Value),
            onFailure: error => error.ToResponse()
        );
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(Guid id, UpdateCustomerRequest request)
    {
        var command = mapper.Map<UpdateCustomerCommand>(request);
        command.Id = id;
        
        var result = await mediator.Send(command);

        return result.Match(
            onSuccess: Ok,
            onFailure: error => error.ToResponse()
        );
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetCustomerQuery(id);
        var result = await mediator.Send(query);

        return result.Match(
            onSuccess: () => Ok(mapper.Map<CustomerResponse>(result.Value)),
            onFailure: error => error.ToResponse()
        );
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResultsResponse<SearchCustomerResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] SearchCustomersRequest request)
    {
        var query = mapper.Map<SearchCustomersQuery>(request);

        var result = await mediator.Send(query);

        return result.Match(
            onSuccess: () => Ok(mapper.Map<PagedResultsResponse<SearchCustomerResponse>>(result.Value)),
            onFailure: error => error.ToResponse()
        );
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteCustomerCommand(id);
        var result = await mediator.Send(command);

        return result.Match(
            onSuccess: Ok,
            onFailure: error => error.ToResponse()
        );
    }
}
