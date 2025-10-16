using FluentValidation;
using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Validators;

public class UpdatePropertyDtoValidator : AbstractValidator<UpdatePropertyDto>
{
    public UpdatePropertyDtoValidator()
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

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.")
            .Must(x => x == "Active" || x == "Inactive").WithMessage("Status must be either 'Active' or 'Inactive'.");

        RuleFor(x => x.HostId)
            .GreaterThan(0).WithMessage("HostId must be greater than 0.");
    }
}
