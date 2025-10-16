using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyManagement.Domain.Entities;

public class DomainEvent
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public string EventType { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string EventData { get; set; } = null!;
    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
    public Property Property { get; set; } = null!;
}
