using KUNAK.VMS.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.QueryDTOs
{
    public class RoleDetailDTO
    {
        public int IdRol { get; set; }
        public int IdCompany { get; set; }
        public string? Name { get; set; }
        public string? CompanyName { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<PermissionDTO>? PermissionsDtos { get; set; }
    }
}
