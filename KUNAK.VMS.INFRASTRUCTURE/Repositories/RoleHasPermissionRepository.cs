using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.INFRASTRUCTURE.Repositories
{
    public class RoleHasPermissionRepository : BaseRepository<RoleHasPermission>, IRoleHasPermissionRepository
    {
        public RoleHasPermissionRepository(VMSContext context) : base(context) { }
        public RoleHasPermission GetByIdConcatenated(int idRol, int idPermission)
        {
            return _entities.Where(x => x.IdRol == idRol)
                .Where(x => x.IdPermission == idPermission).FirstOrDefault();
        }
        public void DeleteByIdConcatenated(int idRol, int idPermission)
        {
            RoleHasPermission entity = GetByIdConcatenated(idRol, idPermission);
            _entities.Remove(entity);
        }
        public bool RolePermissionValidation(int idRol, String permission)
        {
            var rol_permission = _entities.Where(x => x.IdRol == idRol)
                .Where(x => x.IdPermissionNavigation.Name == permission).FirstOrDefault();
            if (rol_permission != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public RoleHasPermission GetByIdTemp(int idRol, int idPermission)
        {
            return _entities.AsNoTracking().Where(x => x.IdRol == idRol && x.IdPermission == idPermission)
                .FirstOrDefault();
        }

        public void DeleteByRole(int idRol)
        {
            _entities.RemoveRange(_entities.Where(x => x.IdRol == idRol));
        }
        public IEnumerable<RoleHasPermission> GetPermissionsByRol(int idRol)
        {
            return _entities.AsNoTracking().Where(x => x.IdRol == idRol).AsEnumerable();
        }
    }
}
