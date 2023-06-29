using API.Utilities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Accounts
{
    public class AccountDto
    {
        [Required]
        public Guid GUID { get; set; }

        [PasswordPolicy]
        public string Password { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [MinLength(6), MaxLength(6)]
        public string? OTP { get; set; }

        public bool IsUsed { get; set; }

        public DateTime ExpiredTime { get; set; }
    }
}
