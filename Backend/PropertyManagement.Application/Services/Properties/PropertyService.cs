using AutoMapper;
using MediatR;
using PropertyManagement.Application.Commands;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Application.Queries;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;

namespace PropertyManagement.Application.Services.Properties;

public class PropertyService : IPropertyService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public PropertyService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PropertyDto>> GetPropertiesAsync(string? name = null, int? hostId = null, string? status = null, int pageNumber = 1, int pageSize = 10)
    {
        var query = new GetPropertiesQuery
        {
            Name = name,
            HostId = hostId,
            Status = status,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        return await _mediator.Send(query);
    }

    public async Task<PropertyDto> GetPropertyByIdAsync(int id)
    {
        var query = new GetPropertyByIdQuery { Id = id };
        return await _mediator.Send(query);
    }

    public async Task<PropertyDto> CreatePropertyAsync(CreatePropertyDto createPropertyDto)
    {
        var command = new CreatePropertyCommand { Property = createPropertyDto };
        return await _mediator.Send(command);
    }

    public async Task<PropertyDto> UpdatePropertyAsync(int id, UpdatePropertyDto updatePropertyDto)
    {
        var command = new UpdatePropertyCommand { Id = id, Property = updatePropertyDto };
        return await _mediator.Send(command);
    }

    public async Task DeletePropertyAsync(int id)
    {
        var command = new DeletePropertyCommand { Id = id };
        await _mediator.Send(command);
    }

    public async Task<DomainEventDto> SyncPropertyAsync(SyncDto syncDto)
    {
        var command = new SyncPropertyCommand
        {
            PropertyId = syncDto.PropertyId,
            Action = syncDto.Action
        };
        return await _mediator.Send(command);
    }
}
