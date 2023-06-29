using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Employees
{
    public class EmployeeDto
    {
        [Required]
        public Guid GUID { get; set; }
        [Required]
        public string NIK { get; set; }
        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }
        [Required]
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
    }
}
