using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Domain.Interfaces;

public interface IDomainEventRepository : IRepository<DomainEvent>
{
    Task AddDomainEventAsync(DomainEvent domainEvent);
}
