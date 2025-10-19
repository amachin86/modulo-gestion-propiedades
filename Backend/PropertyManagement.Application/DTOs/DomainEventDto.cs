using System.ComponentModel.DataAnnotations;

namespace PropertyManagement.Application.DTOs;

public class DomainEventDto
{
    public int Id { get; set; }

    public int PropertyId { get; set; }
    public string? PropertyName { get; set; }

    [Required]
    [MaxLength(100)]
    public string EventType { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    [MaxLength(4000)]
    public string PayloadJSON { get; set; } = string.Empty;
}
