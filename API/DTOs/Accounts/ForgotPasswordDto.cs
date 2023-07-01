using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Accounts;

public class ForgotPasswordDto
{
    [Required]
    public string Email { get; set; }
}

