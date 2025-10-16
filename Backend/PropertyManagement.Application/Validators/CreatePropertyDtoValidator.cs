using FluentValidation;
using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Validators;

public class CreatePropertyDtoValidator : AbstractValidator<CreatePropertyDto>
{
    public CreatePropertyDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MaximumLength(500).WithMessage("Location must not exceed 500 characters.");

        RuleFor(x => x.PricePerNight)
            .GreaterThan(0).WithMessage("Price per night must be greater than 0.");

        RuleFor(x => x.HostId)
            .GreaterThan(0).WithMessage("HostId must be greater than 0.");
    }
}
