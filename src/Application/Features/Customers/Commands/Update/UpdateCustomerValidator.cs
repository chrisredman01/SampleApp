using FluentValidation;

namespace SampleApp.Application.Features.Customers.Commands.Update;

public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerValidator()
    {
        RuleFor(m => m.Name).NotEmpty().MaximumLength(200);
        RuleFor(m => m.Telephone).MaximumLength(20);
        RuleFor(m => m.Email).MaximumLength(320);
        RuleFor(m => m.Email).EmailAddress().MaximumLength(320);
    }
}
