using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Roles
{
    public class RoleDto
    {
        [Required]
        public Guid GUID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
