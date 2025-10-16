using MediatR;
using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Commands;

public class UpdatePropertyCommand : IRequest<PropertyDto>
{
    public int Id { get; set; }
    public UpdatePropertyDto Property { get; set; } = new();
}
