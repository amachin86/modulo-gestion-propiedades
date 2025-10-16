using System.ComponentModel.DataAnnotations;

namespace PropertyManagement.Application.DTOs;

public class CreateBookingDto
{
    [Required]
    public int PropertyId { get; set; }

    [Required]
    public DateTime CheckIn { get; set; }

    [Required]
    public DateTime CheckOut { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal TotalPrice { get; set; }
}
