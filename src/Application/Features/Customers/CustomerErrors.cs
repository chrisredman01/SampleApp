using SampleApp.Application.Common.Models;

namespace SampleApp.Application.Features.Customers;

public static class CustomerErrors
{
    public static Error NotFound(Guid id) => new(ErrorType.NotFound, $"Customer '{id}' not found");
    public static Error IsExisting(string name) => new(ErrorType.BadRequest, $"A customer already exists with name: '{name}'");
}
