using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Services.Properties;

public interface IPropertyService
{
    Task<IEnumerable<PropertyDto>> GetPropertiesAsync(string? name = null, int? hostId = null, string? status = null, int pageNumber = 1, int pageSize = 10);
    Task<PropertyDto> GetPropertyByIdAsync(int id);
    Task<PropertyDto> CreatePropertyAsync(CreatePropertyDto createPropertyDto);
    Task<PropertyDto> UpdatePropertyAsync(int id, UpdatePropertyDto updatePropertyDto);
    Task DeletePropertyAsync(int id);
    Task<DomainEventDto> SyncPropertyAsync(SyncDto syncDto);
}
