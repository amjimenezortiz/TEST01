using KUNAK.VMS.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IRoleHasPermissionRepository : IRepository<RoleHasPermission>
    {
        RoleHasPermission GetByIdConcatenated(int idRol, int idPermission);
        void DeleteByIdConcatenated(int idRol, int idPermission);
        bool RolePermissionValidation(int idRol, String permission);
        RoleHasPermission GetByIdTemp(int idRol, int idPermission);
        void DeleteByRole(int idRol);
        IEnumerable<RoleHasPermission> GetPermissionsByRol(int idRol);
    }
}
