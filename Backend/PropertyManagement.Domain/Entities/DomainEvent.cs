using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyManagement.Domain.Entities;

public class DomainEvent
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid PropertyId { get; set; }

    [ForeignKey(nameof(PropertyId))]
    public Property Property { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string EventType { get; set; } = string.Empty; // SyncWithOTA, etc.

    [MaxLength(1000)]
    public string EventData { get; set; } = string.Empty; // JSON data

    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
}
