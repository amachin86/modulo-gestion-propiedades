using MediatR;
using PropertyManagement.Application.Commands;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Application.Queries;

namespace PropertyManagement.Application.Services.Properties;

public class PropertyService : IPropertyService
{
    private readonly IMediator _mediator;

    public PropertyService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<PropertyDto> CreatePropertyAsync(CreatePropertyDto createPropertyDto)
    {
        var command = new CreatePropertyCommand
        {
            Name = createPropertyDto.Name,
            Description = createPropertyDto.Description,
            HostId = createPropertyDto.HostId,
            Status = createPropertyDto.Status
        };

        return await _mediator.Send(command);
    }

    public async Task<PropertyDto> UpdatePropertyAsync(Guid id, UpdatePropertyDto updatePropertyDto)
    {
        var command = new UpdatePropertyCommand
        {
            Id = id,
            Name = updatePropertyDto.Name,
            Description = updatePropertyDto.Description,
            Status = updatePropertyDto.Status
        };

        return await _mediator.Send(command);
    }

    public async Task DeletePropertyAsync(Guid id)
    {
        var command = new DeletePropertyCommand { Id = id };
        await _mediator.Send(command);
    }

    public async Task<PropertyDto> GetPropertyByIdAsync(Guid id)
    {
        var query = new GetPropertyByIdQuery { Id = id };
        return await _mediator.Send(query);
    }

    public async Task<IEnumerable<PropertyDto>> GetAllPropertiesAsync()
    {
        var query = new GetPropertiesQuery();
        return await _mediator.Send(query);
    }
}
