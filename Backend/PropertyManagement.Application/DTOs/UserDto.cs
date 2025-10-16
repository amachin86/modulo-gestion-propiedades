using System.ComponentModel.DataAnnotations;

namespace PropertyManagement.Application.DTOs;

public class UserDto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Role { get; set; } = "User"; // User, Admin

    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
}
