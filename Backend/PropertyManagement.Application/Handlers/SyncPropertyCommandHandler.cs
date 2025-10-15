using MediatR;
using PropertyManagement.Application.Commands;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;

namespace PropertyManagement.Application.Handlers;

public class SyncPropertyCommandHandler : IRequestHandler<SyncPropertyCommand, Unit>
{
    private readonly IDomainEventRepository _domainEventRepository;

    public SyncPropertyCommandHandler(IDomainEventRepository domainEventRepository)
    {
        _domainEventRepository = domainEventRepository;
    }

    public async Task<Unit> Handle(SyncPropertyCommand request, CancellationToken cancellationToken)
    {
        var domainEvent = new DomainEvent
        {
            PropertyId = request.PropertyId,
            EventType = $"OTA_Sync_{request.OTAType}",
            EventData = $"{{\"action\":\"{request.Action}\",\"ota\":\"{request.OTAType}\"}}",
            OccurredAt = DateTime.UtcNow
        };

        await _domainEventRepository.AddAsync(domainEvent);

        return Unit.Value;
    }
}
