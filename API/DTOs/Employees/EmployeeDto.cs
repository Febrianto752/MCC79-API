using API.Utilities.Enums;

namespace API.DTOs.Employees
{
    public class EmployeeDto
    {
        public Guid GUID { get; set; }
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
