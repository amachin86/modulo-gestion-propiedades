using FluentValidation;
using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Validators;

public class UpdateHostDtoValidator : AbstractValidator<UpdateHostDto>
{
    public UpdateHostDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .MaximumLength(100).WithMessage("Full name must not exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(150).WithMessage("Email must not exceed 150 characters.");

        RuleFor(x => x.Phone)
            .MaximumLength(20).WithMessage("Phone must not exceed 20 characters.");
    }
}
