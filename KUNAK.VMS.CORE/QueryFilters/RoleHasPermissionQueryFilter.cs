using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.QueryFilters
{
    public class RoleHasPermissionQueryFilter
    {
        public int IdRol { get; set; }
        public int IdPermission { get; set; }
        public string Description { get; set; } = null!;
        public bool Status { get; set; }

    }
}
