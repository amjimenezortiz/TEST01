using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IRoleHasPermissionService
    {
        IEnumerable<RoleHasPermission> GetRoleHasPermissions(RoleHasPermissionQueryFilter filters);
        RoleHasPermission GetRoleHasPermission(int idRol, int idPermission);
        Task InsertRoleHasPermission(RoleHasPermission roleHasPermission);
        Task UpdateRoleHasPermission(RoleHasPermission roleHasPermission);
        Task<bool> DeleteRoleHasPermission(int idRol, int idPermission);
        Task UpdatePermissionByRol(int idRol, RoleHasPermission roleHasPermission);
    }
}
