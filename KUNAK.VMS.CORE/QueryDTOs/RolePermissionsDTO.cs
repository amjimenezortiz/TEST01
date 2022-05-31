using KUNAK.VMS.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.QueryDTOs
{
    public class RolePermissionsDTO
    {

        public int IdRol { get; set; }
        public int IdCompany { get; set; }
        public string? Name { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<RoleHasPermissionDTO>? PermissionsDtos { get; set; }
    }
}
