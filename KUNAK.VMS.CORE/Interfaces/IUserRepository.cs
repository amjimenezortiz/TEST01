using KUNAK.VMS.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetUsersCompany();
        IEnumerable<User> GetUsersByCompany(int idCompany);
        Task<User> GetLoginByCredentials(UserLogin user);
        Task<User> GetAditionalPermissions(int idUser);
        User GetUserByEmail(String email);
        User GetUserByDni(String idenDoc);
        //User GetUserByEmail(String email);
        Task<User> GetByIdTemp(int idUser);
        User GetUserDetailById(int idUser);
        bool GetByRol(int idRol);
        User GetByIdUser(int idUser);
        Task<User> GetUserCompany(int idUser);
    }
}
