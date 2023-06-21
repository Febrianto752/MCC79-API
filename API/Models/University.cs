using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    // ini anotasi '[]'
    [Table("tb_m_universities")]
    public class University
    {
        [Key]
        [Column("guid")]
        public Guid GUID { get; set; }

        [Column("code", TypeName = "nvarchar(50)")]
        public string Code { get; set; }

        [Column("name", TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }
    }
}
