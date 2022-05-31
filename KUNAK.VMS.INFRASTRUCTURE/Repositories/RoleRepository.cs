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
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(VMSContext context) : base(context) { }
        public IEnumerable<Role> GetAllRoles()
        {
            var roles = _entities
                .Include(x => x.RoleHasPermissions)
                .ThenInclude(x => x.IdPermissionNavigation)
                .ToList();
            return roles;
        }
        public IEnumerable<Role> GetRolesByCompany(int idCompany)
        {
            return _entities
                .Include(x => x.RoleHasPermissions)
                .ThenInclude(x => x.IdPermissionNavigation)
                .AsEnumerable()
                .Where(x => x.IdCompany == idCompany);
        }
        public async Task<Role> GetRole(int id)
        {
            return await _entities
                .AsNoTracking()
                .Include(x => x.RoleHasPermissions)
                .ThenInclude(x => x.IdPermissionNavigation)
                .Where(x => x.IdRol == id).FirstOrDefaultAsync();
        }
    }
}
