using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyManagement.Domain.Entities;

public class Property
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public Guid HostId { get; set; }

    [ForeignKey(nameof(HostId))]
    public Host Host { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = "Available"; // Available, Occupied, Maintenance

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    public ICollection<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}
