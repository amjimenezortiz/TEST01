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
    public class UsersHasPermissionRepository : BaseRepository<UsersHasPermission>, IUsersHasPermissionRepository
    {
        public UsersHasPermissionRepository(VMSContext context) : base(context) { }
        public bool UserPermissionValidation(int idUser, String permission)
        {
            var user_permission = _entities.Where(x => x.IdUser == idUser)
                .Where(x => x.IdPermissionNavigation.Name == permission).FirstOrDefault();
            if (user_permission != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public UsersHasPermission GetUserPermission(int idUser, int idPermission)
        {
            return _entities.AsNoTracking().Where(x => x.IdUser == idUser && x.IdPermission == idPermission).FirstOrDefault();
        }
        public void DeleteByUser(int idUser)
        {
            _entities.RemoveRange(_entities.Where(x => x.IdUser == idUser));
        }
    }
}
