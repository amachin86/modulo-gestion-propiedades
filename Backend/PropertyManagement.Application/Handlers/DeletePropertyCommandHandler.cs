using MediatR;
using PropertyManagement.Application.Commands;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Domain.Interfaces;

namespace PropertyManagement.Application.Handlers;

public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePropertyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
    {
        var propertyRepository = _unitOfWork.Repository<Property>();
        var property = await propertyRepository.GetByIdAsync(request.Id);

        if (property == null)
        {
            throw new KeyNotFoundException($"Property with ID {request.Id} not found.");
        }

        propertyRepository.Remove(property);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
