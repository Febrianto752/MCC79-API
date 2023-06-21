using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_educations")]
    public class Education : BaseEntity
    {

        [Key]

        [Column("major", TypeName = "nvarchar(100)")]
        public string Major { get; set; }

        [Column("degree", TypeName = "nvarchar(10)")]
        public string Degree { get; set; }

        [Column("gpa", TypeName = "nvarchar(10)")]
        public float GPA { get; set; }

        [Column("university_GUID")]
        public Guid UniversityGUID { get; set; }
    }
}
