using FluentValidation;
using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Validators;

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(100).WithMessage("Username must not exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(150).WithMessage("Email must not exceed 150 characters.");

        RuleFor(x => x.Role)
            .MaximumLength(50).WithMessage("Role must not exceed 50 characters.")
            .Must(x => string.IsNullOrEmpty(x) || x == "User" || x == "Admin").WithMessage("Role must be either 'User' or 'Admin'.");
    }
}
