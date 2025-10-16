using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Domain.Interfaces;

public interface IPropertyRepository : IRepository<Property>
{
    Task<IEnumerable<Property>> GetPropertiesByHostIdAsync(int hostId);
    Task<IEnumerable<Property>> GetPropertiesWithFiltersAsync(string? name, int? hostId, string? status, int pageNumber, int pageSize);
    Task<int> GetTotalPropertiesCountAsync(string? name, int? hostId, string? status);
}
