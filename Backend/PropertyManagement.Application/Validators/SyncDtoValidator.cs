using FluentValidation;
using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Validators;

public class SyncDtoValidator : AbstractValidator<SyncDto>
{
    public SyncDtoValidator()
    {
        RuleFor(x => x.Action)
            .NotEmpty().WithMessage("Action is required.")
            .MaximumLength(100).WithMessage("Action must not exceed 100 characters.");

        RuleFor(x => x.PropertyId)
            .GreaterThan(0).WithMessage("PropertyId must be greater than 0.");

        RuleFor(x => x.Details)
            .MaximumLength(1000).WithMessage("Details must not exceed 1000 characters.");
    }
}
