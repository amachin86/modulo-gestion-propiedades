using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyManagement.Domain.Entities;

public class Property
{
    public int Id { get; set; }
    public int HostId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Location { get; set; } = null!;
    public decimal PricePerNight { get; set; }
    public string Status { get; set; } = "Active"; // Active | Inactive
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public Host Host { get; set; } = null!;
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}
