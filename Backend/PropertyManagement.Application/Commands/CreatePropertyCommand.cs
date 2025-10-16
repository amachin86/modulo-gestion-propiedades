using MediatR;
using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Commands;

public class CreatePropertyCommand : IRequest<PropertyDto>
{
    public CreatePropertyDto Property { get; set; } = new();
}
