using MediatR;
using PropertyManagement.Application.Commands;

namespace PropertyManagement.Application.Services.Sync;

public class SyncService : ISyncService
{
    private readonly IMediator _mediator;

    public SyncService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task SyncPropertyAsync(Guid propertyId, string action)
    {
        var command = new SyncPropertyCommand
        {
            PropertyId = propertyId,
            Action = action
        };

        await _mediator.Send(command);
    }
}
