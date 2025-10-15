using Microsoft.EntityFrameworkCore;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;
using PropertyManagement.Infrastructure.Data;

namespace PropertyManagement.Infrastructure.Repositories;

public class DomainEventRepository : Repository<DomainEvent>, IDomainEventRepository
{
    public DomainEventRepository(ApplicationDbContext context) : base(context) { }

    public async Task AddDomainEventAsync(DomainEvent domainEvent)
    {
        await _context.DomainEvents.AddAsync(domainEvent);
        await _context.SaveChangesAsync();
    }
}
