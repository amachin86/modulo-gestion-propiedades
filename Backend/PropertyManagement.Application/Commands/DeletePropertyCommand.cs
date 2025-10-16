using MediatR;

namespace PropertyManagement.Application.Commands;

public class DeletePropertyCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
