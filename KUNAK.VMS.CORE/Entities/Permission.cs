using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class Permission : BaseEntity
    {
        public Permission()
        {
            RoleHasPermissions = new HashSet<RoleHasPermission>();
            UsersHasPermissions = new HashSet<UsersHasPermission>();
        }

        public int IdPermission { get; set; }
        public string Name { get; set; } = null!;
        public bool? Status { get; set; }
        public bool ForClient { get; set; }

        public virtual ICollection<RoleHasPermission> RoleHasPermissions { get; set; }
        public virtual ICollection<UsersHasPermission> UsersHasPermissions { get; set; }
    }
}
