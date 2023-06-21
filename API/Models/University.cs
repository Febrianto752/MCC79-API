using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    // ini anotasi '[]'
    [Table("tb_m_universities")]
    public class University : BaseEntity
    {
        [Key]

        [Column("code", TypeName = "nvarchar(50)")]
        public string Code { get; set; }

        [Column("name", TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        // cardinality
        public ICollection<Education> Educations { get; set; }
    }
}
