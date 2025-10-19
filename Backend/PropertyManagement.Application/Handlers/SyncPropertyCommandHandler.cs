using AutoMapper;
using MediatR;
using PropertyManagement.Application.Commands;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;

namespace PropertyManagement.Application.Handlers;

public class SyncPropertyCommandHandler : IRequestHandler<SyncPropertyCommand, DomainEventDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SyncPropertyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DomainEventDto> Handle(SyncPropertyCommand request, CancellationToken cancellationToken)
    {
        var domainEvent = new DomainEvent
        {
            PropertyId = request.PropertyId,
            EventType = request.Action,
            PayloadJSON = "{}", // Empty JSON object for now
            CreatedAt = DateTime.UtcNow
        };

        var domainEventRepository = _unitOfWork.Repository<DomainEvent>();
        await domainEventRepository.AddAsync(domainEvent);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<DomainEventDto>(domainEvent);
    }
}
