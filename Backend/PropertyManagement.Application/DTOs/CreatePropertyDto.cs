using FluentValidation;

namespace PropertyManagement.Application.DTOs;

public class CreatePropertyDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid HostId { get; set; }
    public string Status { get; set; } = "Available";
}

public class CreatePropertyDtoValidator : AbstractValidator<CreatePropertyDto>
{
    public CreatePropertyDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters");

        RuleFor(x => x.HostId)
            .NotEmpty().WithMessage("HostId is required");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required")
            .Must(status => new[] { "Available", "Occupied", "Maintenance" }.Contains(status))
            .WithMessage("Status must be one of: Available, Occupied, Maintenance");
    }
}
