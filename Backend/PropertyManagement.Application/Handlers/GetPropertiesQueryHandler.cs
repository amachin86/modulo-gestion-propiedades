using AutoMapper;
using MediatR;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Application.Queries;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;

namespace PropertyManagement.Application.Handlers;

public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, IEnumerable<PropertyDto>>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IMapper _mapper;

    public GetPropertiesQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
    {
        _propertyRepository = propertyRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PropertyDto>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
    {
        var properties = await _propertyRepository.GetPropertiesWithFiltersAsync(
            request.Name,
            request.HostId,
            request.Status,
            request.PageNumber,
            request.PageSize);

        return _mapper.Map<IEnumerable<PropertyDto>>(properties);
    }
}
