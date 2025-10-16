using MediatR;
using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Commands;

public class SyncPropertyCommand : IRequest<DomainEventDto>
{
    public int PropertyId { get; set; }
    public string Action { get; set; } = string.Empty;
}
