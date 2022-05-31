using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAllRoles(RoleQueryFilter filters);

        IEnumerable<Role> GetRolesByCompany(RoleQueryFilter filters, int idCompany);
        Task<Role> GetRole(int id);
        Task InsertRole(Role role);
        Task UpdateRole(Role role);
        Task<bool> DeleteRole(int id);
    }
}
