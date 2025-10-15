using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Domain.Interfaces;

public interface IPropertyRepository : IRepository<Property>
{
    Task<IEnumerable<Property>> GetPropertiesByHostIdAsync(Guid hostId);
    Task<IEnumerable<Property>> GetPropertiesWithFiltersAsync(string? name, Guid? hostId, string? status, int pageNumber, int pageSize);
    Task<int> GetTotalPropertiesCountAsync(string? name, Guid? hostId, string? status);
}
