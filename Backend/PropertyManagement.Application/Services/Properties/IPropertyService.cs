using PropertyManagement.Application.DTOs;

namespace PropertyManagement.Application.Services.Properties;

public interface IPropertyService
{
    Task<PropertyDto> CreatePropertyAsync(CreatePropertyDto createPropertyDto);
    Task<PropertyDto> UpdatePropertyAsync(Guid id, UpdatePropertyDto updatePropertyDto);
    Task DeletePropertyAsync(Guid id);
    Task<PropertyDto> GetPropertyByIdAsync(Guid id);
    Task<IEnumerable<PropertyDto>> GetAllPropertiesAsync();
}
