using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Application.Queries;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;

namespace PropertyManagement.Application.Handlers;

public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, IEnumerable<PropertyDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPropertiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PropertyDto>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
    {
        var propertyRepository = _unitOfWork.Repository<Property>();
        var properties = await propertyRepository.GetAllAsync();

        // Apply filters
        if (!string.IsNullOrEmpty(request.Name))
        {
            properties = properties.Where(p => p.Name.Contains(request.Name));
        }

        if (request.HostId.HasValue)
        {
            properties = properties.Where(p => p.HostId == request.HostId.Value);
        }

        if (!string.IsNullOrEmpty(request.Status))
        {
            properties = properties.Where(p => p.Status == request.Status);
        }

        // Apply pagination
        properties = properties.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

        return _mapper.Map<IEnumerable<PropertyDto>>(properties);
    }
}
