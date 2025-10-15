using MediatR;

namespace PropertyManagement.Application.Commands;

public class DeletePropertyCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
