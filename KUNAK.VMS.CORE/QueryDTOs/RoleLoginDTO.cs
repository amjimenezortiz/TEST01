using KUNAK.VMS.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.QueryDTOs
{
    public class RoleLoginDTO
    {
        public int IdUser { get; set; }
        public string? IdenDoc { get; set; }
        public string? LastName { get; set; }
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int IdRol { get; set; }
        public bool Status { get; set; }
        public int IdCompany { get; set; }
        public string? RoleName { get; set; }
        public virtual ICollection<PermissionDTO>? RolePermissions { get; set; }
        public virtual ICollection<PermissionDTO>? UserPermissions { get; set; }
    }
}
