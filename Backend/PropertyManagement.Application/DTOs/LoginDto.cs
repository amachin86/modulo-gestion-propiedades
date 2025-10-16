using System.ComponentModel.DataAnnotations;

namespace PropertyManagement.Application.DTOs;

public class LoginDto
{
    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
}
