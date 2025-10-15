using MediatR;
using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Queries;

public class GetPropertyByIdQuery : IRequest<PropertyDto?>
{
    public Guid Id { get; set; }
}
