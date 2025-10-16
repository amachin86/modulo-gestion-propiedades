using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyManagement.Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public int UserId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public decimal TotalPrice { get; set; }

    public Property Property { get; set; } = null!;
    public User User { get; set; } = null!;
}
