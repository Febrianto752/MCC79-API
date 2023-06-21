using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_tr_account_roles")]
    public class AccountRole
    {

        [Key]

        [Column("guid")]
        public Guid GUID { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }

        [Column("account_guid")]
        public Guid AccountGUID { get; set; }

        [Column("role_guid")]
        public Guid RoleGUID { get; set; }
    }
}
