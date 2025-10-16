using System.ComponentModel.DataAnnotations;

namespace PropertyManagement.Application.DTOs;

public class BookingDto
{
    public int Id { get; set; }

    public int PropertyId { get; set; }
    public string? PropertyName { get; set; }

    [Required]
    public DateTime CheckIn { get; set; }

    [Required]
    public DateTime CheckOut { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal TotalPrice { get; set; }
}
