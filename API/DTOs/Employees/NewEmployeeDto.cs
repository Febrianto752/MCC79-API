using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Employees
{
    public class NewEmployeeDto
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        [Range(0, 1, ErrorMessage = "Must be Female or Male")]
        public GenderEnum Gender { get; set; }
        public DateTime HiringDate { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
