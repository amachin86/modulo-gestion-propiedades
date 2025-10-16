using Microsoft.EntityFrameworkCore;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;
using PropertyManagement.Infrastructure.Data;

namespace PropertyManagement.Infrastructure.Repositories;

public class PropertyRepository : Repository<Property>, IPropertyRepository
{
    public PropertyRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Property>> GetPropertiesByHostIdAsync(int hostId)
    {
        return await _context.Properties
            .Where(p => p.HostId == hostId)
            .Include(p => p.Host)
            .ToListAsync();
    }

    public async Task<IEnumerable<Property>> GetPropertiesWithFiltersAsync(
        string? name,
        int? hostId,
        string? status,
        int pageNumber,
        int pageSize)
    {
        var query = _context.Properties.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(p => p.Name.Contains(name));
        }

        if (hostId.HasValue)
        {
            query = query.Where(p => p.HostId == hostId.Value);
        }

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(p => p.Status == status);
        }

        return await query
            .Include(p => p.Host)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalPropertiesCountAsync(string? name, int? hostId, string? status)
    {
        var query = _context.Properties.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(p => p.Name.Contains(name));
        }

        if (hostId.HasValue)
        {
            query = query.Where(p => p.HostId == hostId.Value);
        }

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(p => p.Status == status);
        }

        return await query.CountAsync();
    }
}
