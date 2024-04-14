using FluentValidation;
using MediatR;

namespace SampleApp.Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var validationContext = new ValidationContext<TRequest>(request);

            var validationTasks = validators.Select(validator => validator.ValidateAsync(validationContext, cancellationToken));
            var validationResults = await Task.WhenAll(validationTasks);

            var failures = validationResults
                .Where(result => result.Errors.Any())
                .SelectMany(result => result.Errors)
                .ToList();

            if (failures.Count > 0)
            {
                throw new ValidationException(failures);
            }
        }

        return await next();
    }
}
