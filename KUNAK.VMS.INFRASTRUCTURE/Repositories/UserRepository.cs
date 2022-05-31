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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(VMSContext context) : base(context) { }

        public IEnumerable<User> GetUsersCompany()
        {
            return _entities.Include(x => x.IdRolNavigation.IdCompanyNavigation)
                .Include(x => x.UsersHasPermissions)
                .ThenInclude(x => x.IdPermissionNavigation)
                .Include(x => x.IdRolNavigation)
                .ThenInclude(x => x.RoleHasPermissions)
                .ThenInclude(x => x.IdPermissionNavigation)
                .AsEnumerable();
        }
        public IEnumerable<User> GetUsersByCompany(int idCompany)
        {
            return _entities.Include(x => x.IdRolNavigation.IdCompanyNavigation)
                .Include(x => x.UsersHasPermissions)
                .ThenInclude(x => x.IdPermissionNavigation)
                .Include(x => x.IdRolNavigation)
                .ThenInclude(x => x.RoleHasPermissions)
                .ThenInclude(x => x.IdPermissionNavigation)
                .AsEnumerable().Where(x => x.IdRolNavigation.IdCompany == idCompany);
        }
        public async Task<User> GetLoginByCredentials(UserLogin user)
        {
            return await _entities.Include(x => x.IdRolNavigation)
                                  .ThenInclude(x => x.RoleHasPermissions)
                                  .ThenInclude(x => x.IdPermissionNavigation)
                                  .FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password);
        }
        public async Task<User> GetAditionalPermissions(int idUser)
        {
            return await _entities
                .Include(x => x.UsersHasPermissions)
                .ThenInclude(x => x.IdPermissionNavigation)
                .FirstOrDefaultAsync(x => x.IdUser == idUser);
        }

        public User GetUserByEmail(String email)
        {
            return _entities.AsNoTracking()
                .Include(x => x.UsersHasPermissions)
                .ThenInclude(x => x.IdPermissionNavigation)
                .Include(x => x.IdRolNavigation)
                .ThenInclude(x => x.RoleHasPermissions)
                .ThenInclude(x => x.IdPermissionNavigation)
                .Where(x => x.Email == email).FirstOrDefault();
            //return _entities.Where(x => x.Username == userName).FirstOrDefault();
        }

        public User GetUserByDni(String idenDoc)
        {
            return _entities.AsNoTracking().Where(x => x.IdenDoc == idenDoc).FirstOrDefault();
        }

        public async Task<User> GetByIdTemp(int idUser)
        {
            return await _entities.AsNoTracking().FirstAsync(x => x.IdUser == idUser);
        }

        //public User GetUserByEmail(String email)
        //{
        //    return _entities.AsNoTracking().Where(x => x.Email == email).FirstOrDefault();
        //}

        public User GetUserDetailById(int idUser)
        {
            return _entities.AsNoTracking()
                .Include(x => x.IdRolNavigation.IdCompanyNavigation)
                .Include(x => x.UsersHasPermissions)
                .ThenInclude(x => x.IdPermissionNavigation)
                .Include(x => x.IdRolNavigation)
                .ThenInclude(x => x.RoleHasPermissions)
                .ThenInclude(x => x.IdPermissionNavigation)
                .Where(x => x.IdUser == idUser).FirstOrDefault();
        }
        public bool GetByRol(int idRol)
        {
            var users = _entities.Where(x => x.IdRol == idRol).Count();
            if (users > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public User GetByIdUser(int idUser)
        {
            return _entities.AsNoTracking().First(x => x.IdUser == idUser);
        }

        public async Task<User> GetUserCompany(int idUser)
        {
            return await _entities.Include(x => x.IdRolNavigation.IdCompanyNavigation)
                //.Include(x => x.IdRolNavigation)
                .AsNoTracking().FirstAsync(x => x.IdUser == idUser);
        }
    }
}
