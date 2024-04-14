using FluentValidation;

namespace SampleApp.Application.Features.Customers.Queries.Get;

public class GetCustomerValidator : AbstractValidator<GetCustomerQuery>
{
    public GetCustomerValidator()
    {
        RuleFor(m => m.Id).NotEmpty();
    }
}
