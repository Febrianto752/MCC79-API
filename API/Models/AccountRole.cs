using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_tr_account_roles")]
    public class AccountRole : BaseEntity
    {
        [Column("account_guid")]
        public Guid AccountGUID { get; set; }

        [Column("role_guid")]
        public Guid RoleGUID { get; set; }

        // Cardinality
        public Account? Account { get; set; }

        public Role? Role { get; set; }
    }
}
