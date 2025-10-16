using FluentValidation;
using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Validators;

public class UpdateBookingDtoValidator : AbstractValidator<UpdateBookingDto>
{
    public UpdateBookingDtoValidator()
    {
        RuleFor(x => x.PropertyId)
            .GreaterThan(0).WithMessage("PropertyId must be greater than 0.");

        RuleFor(x => x.CheckIn)
            .NotEmpty().WithMessage("Check-in date is required.")
            .GreaterThan(DateTime.Now).WithMessage("Check-in date must be in the future.");

        RuleFor(x => x.CheckOut)
            .NotEmpty().WithMessage("Check-out date is required.")
            .GreaterThan(x => x.CheckIn).WithMessage("Check-out date must be after check-in date.");

        RuleFor(x => x.TotalPrice)
            .GreaterThan(0).WithMessage("Total price must be greater than 0.");
    }
}
