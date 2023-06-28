using System.ComponentModel.DataAnnotations;

namespace API.DTOs.AccountRoles
{
    public class NewAccountRoleDto
    {
        [Required]
        public Guid AccountGUID { get; set; }
        [Required]
        public Guid RoleGUID { get; set; }
    }
}
