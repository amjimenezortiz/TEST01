using KUNAK.VMS.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IUsersHasPermissionRepository : IRepository<UsersHasPermission>
    {
        bool UserPermissionValidation(int idUser, String permission);
        UsersHasPermission GetUserPermission(int idUser, int idPermission);
        void DeleteByUser(int idUser);
    }
}
