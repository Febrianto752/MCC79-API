using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Auth;

public class SigninDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}

