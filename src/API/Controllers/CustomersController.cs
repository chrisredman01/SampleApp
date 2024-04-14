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

namespace SampleApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(CreateCustomerCommand command)
    {
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
    public async Task<IActionResult> Put(UpdateCustomerCommand command)
    {
        var result = await mediator.Send(command);

        return result.Match(
            onSuccess: Ok,
            onFailure: error => error.ToResponse()
        );
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetCustomerQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetCustomerQuery(id);
        var result = await mediator.Send(query);

        return result.Match(
            onSuccess: () => Ok(result.Value),
            onFailure: error => error.ToResponse()
        );
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResults<SearchCustomersQueryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] SearchCustomersQuery query)
    {
        var result = await mediator.Send(query);

        return result.Match(
            onSuccess: () => Ok(result.Value),
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
