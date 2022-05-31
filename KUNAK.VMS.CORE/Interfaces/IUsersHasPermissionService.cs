using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IUsersHasPermissionService
    {
        IEnumerable<UsersHasPermission> GetUsersHasPermissions(UsersHasPermissionQueryFilter filters);
        Task<UsersHasPermission> GetUsersHasPermission(int id);
        Task InsertUsersHasPermission(UsersHasPermission usersHasPermission);
        Task UpdateUsersHasPermission(UsersHasPermission usersHasPermission);
        Task<bool> DeleteUsersHasPermission(int id);
        //void AddUserHasPermissions(UsersHasPermissionDTO usersHasPermissionDTO);
    }
}
