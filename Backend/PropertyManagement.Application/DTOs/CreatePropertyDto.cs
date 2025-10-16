using System.ComponentModel.DataAnnotations;

namespace PropertyManagement.Application.DTOs;

public class CreatePropertyDto
{
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
    public int HostId { get; set; }
}
