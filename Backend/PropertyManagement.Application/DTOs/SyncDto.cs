using System.ComponentModel.DataAnnotations;

namespace PropertyManagement.Application.DTOs;

public class SyncDto
{
    [Required]
    [MaxLength(100)]
    public string Action { get; set; } = string.Empty; // e.g., "SyncToAirbnb", "SyncToBooking"

    [Required]
    public int PropertyId { get; set; }

    [MaxLength(1000)]
    public string Details { get; set; } = string.Empty;
}
