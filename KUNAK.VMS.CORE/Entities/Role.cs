using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class Role : BaseEntity
    {
        public Role()
        {
            RoleHasPermissions = new HashSet<RoleHasPermission>();
            Users = new HashSet<User>();
        }

        public int IdRol { get; set; }
        public string Name { get; set; } = null!;
        public bool? Status { get; set; }
        public int IdCompany { get; set; }

        public virtual Company IdCompanyNavigation { get; set; } = null!;
        public virtual ICollection<RoleHasPermission> RoleHasPermissions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
