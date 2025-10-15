using MediatR;
using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Commands;

public class UpdatePropertyCommand : IRequest<PropertyDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid HostId { get; set; }
    public string Status { get; set; } = string.Empty;
}
