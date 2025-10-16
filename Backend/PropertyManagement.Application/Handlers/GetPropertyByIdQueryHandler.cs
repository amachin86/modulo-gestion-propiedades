using AutoMapper;
using MediatR;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Application.Queries;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;

namespace PropertyManagement.Application.Handlers;

public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, PropertyDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPropertyByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PropertyDto> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
    {
        var propertyRepository = _unitOfWork.Repository<Property>();
        var property = await propertyRepository.GetByIdAsync(request.Id);

        if (property == null)
        {
            throw new KeyNotFoundException($"Property with ID {request.Id} not found.");
        }

        return _mapper.Map<PropertyDto>(property);
    }
}
