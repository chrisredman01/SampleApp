using FluentValidation;

namespace SampleApp.Application.Features.Customers.Commands.Delete;

public class DeleteCustomerValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerValidator()
    {
        RuleFor(m => m.Id).NotEmpty();
    }
}
