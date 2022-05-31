using KUNAK.VMS.CORE.CustomEntities;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IUserService
    {
        PagedList<User> GetAllUsersDetails(UserQueryFilter filters);

        PagedList<User> GetUsersByCompany(UserQueryFilter filters, int idCompany);
        Task<User> GetUser(int id);
        Task InsertUser(User user);
        Task UpdateUser(User user, String email);
        Task<bool> DeleteUser(int id);
        Task<User> GetLogiByCredentials(UserLogin User);
        Task<User> GetAditionalPermissions(int idUser);
        User GetUserByEmail(String email);
        bool UserPermissionValidation(int idUser, int idRol, String permission);
        bool RolePermissionValidation(int idRol, String permission);
        bool UserPermissionValidation(int idUser, String permission);
        User GetUserDetailById(int idUser);
        string GetSHA256(String password);
        Task ResetPassword(User user);
        User GetByIdUser(int id);
    }
}
