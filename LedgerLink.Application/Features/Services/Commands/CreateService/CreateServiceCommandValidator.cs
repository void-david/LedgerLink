using System.Data;
using FluentValidation;

namespace LedgerLink.Application.Features.Services.Commands.CreateService;

public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Service name is required.")
            .MaximumLength(100);

        RuleFor(v => v.BasePrice)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");
    }
}