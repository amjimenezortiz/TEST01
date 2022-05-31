using KUNAK.VMS.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        IEnumerable<Role> GetAllRoles();
        IEnumerable<Role> GetRolesByCompany(int idCompany);
        Task<Role> GetRole(int id);
    }
}
