using AutoMapper;
using MediatR;
using PropertyManagement.Application.Commands;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;

namespace PropertyManagement.Application.Handlers;

public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, PropertyDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdatePropertyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PropertyDto> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
    {
        var propertyRepository = _unitOfWork.Repository<Property>();
        var property = await propertyRepository.GetByIdAsync(request.Id);

        if (property == null)
        {
            throw new KeyNotFoundException($"Property with ID {request.Id} not found.");
        }

        _mapper.Map(request.Property, property);
        property.UpdatedAt = DateTime.UtcNow;

        propertyRepository.Update(property);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<PropertyDto>(property);
    }
}
