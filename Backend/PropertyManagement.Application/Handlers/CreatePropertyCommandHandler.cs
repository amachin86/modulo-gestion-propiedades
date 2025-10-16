using AutoMapper;
using MediatR;
using PropertyManagement.Application.Commands;
using PropertyManagement.Application.DTOs;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;

namespace PropertyManagement.Application.Handlers;

public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, PropertyDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePropertyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PropertyDto> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        var property = _mapper.Map<Property>(request.Property);
        property.CreatedAt = DateTime.UtcNow;
        property.UpdatedAt = DateTime.UtcNow;
        property.Status = "Active";

        var propertyRepository = _unitOfWork.Repository<Property>();
        await propertyRepository.AddAsync(property);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<PropertyDto>(property);
    }
}
