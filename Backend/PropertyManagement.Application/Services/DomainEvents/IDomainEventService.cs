using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Services.DomainEvents;

public interface IDomainEventService
{
    Task<IEnumerable<DomainEventDto>> GetDomainEventsAsync(int? propertyId = null, string? eventType = null, DateTime? occurredAt = null, int pageNumber = 1, int pageSize = 10);
    Task<DomainEventDto> GetDomainEventByIdAsync(int id);
    Task<DomainEventDto> CreateDomainEventAsync(DomainEventDto domainEventDto);
    Task DeleteDomainEventAsync(int id);
}
