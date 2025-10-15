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
        var property = await _unitOfWork.Repository<Property>().GetByIdAsync(request.Id);
        if (property == null)
        {
            throw new KeyNotFoundException($"Property with ID {request.Id} not found");
        }

        property.Name = request.Name;
        property.Description = request.Description;
        property.HostId = request.HostId;
        property.Status = request.Status;
        property.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Repository<Property>().Update(property);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<PropertyDto>(property);
    }
}
