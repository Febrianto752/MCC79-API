using API.Utilities;
using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Accounts;

public class RegisterDto
{
    [Required]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required]
    public DateTime Birthdate { get; set; }
    [Required]
    [Range(0, 1, ErrorMessage = "Must be Female or Male")]
    public GenderEnum Gender { get; set; }
    [Required]
    public DateTime HiringDate { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
    [Required]
    public string Major { get; set; }
    [Required]
    public string Degree { get; set; }
    [Required]
    [Range(0, 4, ErrorMessage = "GPA must betwen 0 to 4")]
    public float GPA { get; set; }
    [Required]
    public string UniversityCode { get; set; }
    [Required]
    public string UniversityName { get; set; }
    [Required]
    [PasswordPolicy]
    public string Password { get; set; }
    [Required]
    [ConfirmPassword("Password", ErrorMessage = "Password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

}

