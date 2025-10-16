using MediatR;
using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Queries;

public class GetPropertiesQuery : IRequest<IEnumerable<PropertyDto>>
{
    public string? Name { get; set; }
    public int? HostId { get; set; }
    public string? Status { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
