using MediatR;

namespace PropertyManagement.Application.Commands;

public class SyncPropertyCommand : IRequest<Unit>
{
    public Guid PropertyId { get; set; }
    public string OTAType { get; set; } = string.Empty; // Airbnb, Booking, etc.
    public string Action { get; set; } = string.Empty; // Sync, Update, etc.
}
