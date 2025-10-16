using MediatR;
using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Queries;

public class GetPropertyByIdQuery : IRequest<PropertyDto>
{
    public int Id { get; set; }
}
