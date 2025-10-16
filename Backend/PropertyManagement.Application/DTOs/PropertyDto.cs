using System.ComponentModel.DataAnnotations;

namespace PropertyManagement.Application.DTOs;

public class PropertyDto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Location { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal PricePerNight { get; set; }

    [Required]
    public string Status { get; set; } = "Active"; // Active, Inactive

    public int HostId { get; set; }
    public string? HostName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
