using Microsoft.AspNetCore.Mvc;
using SampleApp.Application.Common.Models;

namespace SampleApp.API.Extensions;

public static class ErrorExtensions
{
    public static IActionResult ToResponse(this Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.BadRequest => StatusCodes.Status400BadRequest,
            ErrorType.ServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Type = "https://tools.ietf.org/html/rfc7231",
            Title = error.Description
        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = statusCode
        };
    }
}
