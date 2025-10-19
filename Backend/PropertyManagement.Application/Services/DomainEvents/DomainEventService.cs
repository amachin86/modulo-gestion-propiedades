using AutoMapper;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;

namespace PropertyManagement.Application.Services.DomainEvents;

public class DomainEventService : IDomainEventService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DomainEventService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DomainEventDto>> GetDomainEventsAsync(int? propertyId = null, string? eventType = null, DateTime? occurredAt = null, int pageNumber = 1, int pageSize = 10)
    {
        var domainEventRepository = _unitOfWork.Repository<DomainEvent>();
        var domainEvents = await domainEventRepository.GetAllAsync();

        // Apply filters
        if (propertyId.HasValue)
        {
            domainEvents = domainEvents.Where(de => de.PropertyId == propertyId.Value);
        }

        if (!string.IsNullOrEmpty(eventType))
        {
            domainEvents = domainEvents.Where(de => de.EventType.Contains(eventType, StringComparison.OrdinalIgnoreCase));
        }

        if (occurredAt.HasValue)
        {
            domainEvents = domainEvents.Where(de => de.CreatedAt.Date == occurredAt.Value.Date);
        }

        // Apply pagination
        domainEvents = domainEvents.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        return _mapper.Map<IEnumerable<DomainEventDto>>(domainEvents);
    }

    public async Task<DomainEventDto> GetDomainEventByIdAsync(int id)
    {
        var domainEventRepository = _unitOfWork.Repository<DomainEvent>();
        var domainEvent = await domainEventRepository.GetByIdAsync(id);

        if (domainEvent == null)
        {
            throw new KeyNotFoundException($"DomainEvent with ID {id} not found.");
        }

        return _mapper.Map<DomainEventDto>(domainEvent);
    }

    public async Task<DomainEventDto> CreateDomainEventAsync(DomainEventDto domainEventDto)
    {
        var domainEvent = _mapper.Map<DomainEvent>(domainEventDto);

        var domainEventRepository = _unitOfWork.Repository<DomainEvent>();
        await domainEventRepository.AddAsync(domainEvent);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<DomainEventDto>(domainEvent);
    }

    public async Task DeleteDomainEventAsync(int id)
    {
        var domainEventRepository = _unitOfWork.Repository<DomainEvent>();
        var domainEvent = await domainEventRepository.GetByIdAsync(id);

        if (domainEvent == null)
        {
            throw new KeyNotFoundException($"DomainEvent with ID {id} not found.");
        }

        domainEventRepository.Remove(domainEvent);
        await _unitOfWork.SaveChangesAsync();
    }
}
