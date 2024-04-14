using FluentValidation;

namespace SampleApp.Application.Features.Customers.Commands.Create;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(m => m.Name).NotEmpty().MaximumLength(200);
        RuleFor(m => m.Telephone).MaximumLength(20);
        RuleFor(m => m.Email).EmailAddress().MaximumLength(320);
    }
}
