using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IPermissionService
    {
        IEnumerable<Permission> GetPermissions(PermissionQueryFilter filters);
        IEnumerable<Permission> GetPermissionsForClient(PermissionQueryFilter filters);
        Task<Permission> GetPermission(int id);
        Task InsertPermission(Permission permission);
        Task UpdatePermission(Permission permission);
        Task<bool> DeletePermission(int id);
        IEnumerable<Permission> GetPermissionsAsync();
        IEnumerable<Permission> GetPermissionsAditional(int idUser);
    }
}
