using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class RoleHasPermission : BaseEntity
    {
        public bool? Status { get; set; }
        public int IdPermission { get; set; }
        public int IdRol { get; set; }

        public virtual Permission? IdPermissionNavigation { get; set; }
        public virtual Role? IdRolNavigation { get; set; }
    }
}
