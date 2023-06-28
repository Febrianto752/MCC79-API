using System.ComponentModel.DataAnnotations;

namespace API.DTOs.AccountRoles
{
    public class AccountRoleDto
    {
        [Required]
        public Guid GUID { get; set; }
        [Required]
        public Guid AccountGUID { get; set; }
        [Required]
        public Guid RoleGUID { get; set; }
    }
}
