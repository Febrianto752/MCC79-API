using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    // ini anotasi '[]'
    [Table("tb_m_accounts")]
    public class Account
    {
        [Key]

        [Column("guid")]
        public Guid GUID { get; set; }

        [Column("password", TypeName = "nvarchar(255)")]
        public string Password { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("otp", TypeName = "nvarchar(6)")]
        public string OTP { get; set; }

        [Column("is_used")]
        public bool IsUsed { get; set; }

        [Column("expired_time")]
        public DateTime ExpiredTime { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }
    }
}
