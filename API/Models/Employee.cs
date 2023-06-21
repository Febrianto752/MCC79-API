using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    // ini anotasi '[]'
    [Table("tb_m_employees")]
    public class Employee : BaseEntity
    {

        [Key]

        [Column("nik", TypeName = "nvarchar(6)")]
        public string NIK { get; set; }

        [Column("first_name", TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [Column("last_name", TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        [Column("birthdate")]
        public DateTime Birthdate { get; set; }

        // create gender here

        [Column("hiring_date")]
        public DateTime HiringDate { get; set; }

        [Column("email", TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        [Column("phone_number", TypeName = "nvarchar(20)")]
        public string PhoneNumber { get; set; }

    }
}
