﻿using API.Utilities.Validations;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Auth
{
    public class ChangePasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Otp { get; set; }
        [PasswordPolicy]
        public string NewPassword { get; set; }
        [ConfirmPassword("NewPassword", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
