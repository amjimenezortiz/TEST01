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
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(VMSContext context) : base(context) { }
        public IEnumerable<Permission> GetPermissionsForClient()
        {
            return _entities.Where(x => x.ForClient == true).AsEnumerable();
        }
        public IEnumerable<Permission> GetPermissionsAsync()
        {
            return _entities.AsNoTracking().AsEnumerable();
        }
    }
}
