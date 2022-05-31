using KUNAK.VMS.CORE.CustomEntities;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Exceptions;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.CORE.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public UserService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<User> GetAllUsersDetails(UserQueryFilter filters)
        {
            filters.Page = filters.Page == 0 ? _paginationOptions.DefaultPageNumber : filters.Page;
            filters.Size = filters.Size == 0 ? _paginationOptions.DefaultPageSize : filters.Size;

            var users = _unitOfWork.UserRepository.GetUsersCompany();


            if (filters.Name != null)
            {
                users = users.Where(x => x.Name.ToLower().Contains(filters.Name.ToLower()));
            }
            if (filters.Sort != null && filters.Order != null)
            {
                switch (filters.Sort.ToLower())
                {
                    case "name":
                        if (filters.Order == "asc") users = users.OrderBy(x => x.Name); else users = users.OrderByDescending(x => x.Name);
                        break;
                    case "tradename":
                        if (filters.Order == "asc") users = users.OrderBy(x => x.IdRolNavigation.IdCompanyNavigation.TradeName); else users = users.OrderByDescending(x => x.IdRolNavigation.IdCompanyNavigation.TradeName);
                        break;
                    case "email":
                        if (filters.Order == "asc") users = users.OrderBy(x => x.Email); else users = users.OrderByDescending(x => x.Email);
                        break;
                    default:
                        break;
                }
            }
            var pagedusers = PagedList<User>.Create(users, filters.Page, filters.Size);
            return pagedusers;
        }


        public async Task<User> GetUser(int id)
        {
            return await _unitOfWork.UserRepository.GetUserCompany(id);
        }

        public User GetByIdUser(int id)
        {
            return _unitOfWork.UserRepository.GetByIdUser(id);
        }

        public async Task InsertUser(User user)
        {
            //Validate if company Id was registered before
            //var idCompanyValidate = await _unitOfWork.CompanyRepository.GetById(user.IdCompany);

            //if (idCompanyValidate == null)
            //{
            //    throw new BusinessException("La compañía no se encuentra registrada");
            //}
            //Validate if Email was registered before
            var emailValidate = _unitOfWork.UserRepository.GetUserByEmail(user.Email);
            if (emailValidate != null)
            {
                throw new BusinessException("El Email ya se encuentra registrado");
            }
            //Validate if Dni was registered before
            var dniValidate = _unitOfWork.UserRepository.GetUserByDni(user.IdenDoc);
            if (dniValidate != null)
            {
                throw new BusinessException("El Dni ya se encuentra registrado");
            }
            //Validar
            user.Password = GetSHA256(user.Password);
            await _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task UpdateUser(User user, String email)
        {
            //Validate if company Id was registered before
            //var idCompanyValidate = await _unitOfWork.CompanyRepository.GetById(user.IdCompany);

            //if (idCompanyValidate == null)
            //{
            //    throw new BusinessException("La compañía no se encuentra registrada");
            //}
            //Validate if Email was registered before
            var emailValidate = _unitOfWork.UserRepository.GetUserByEmail(user.Email);
            if (emailValidate != null && user.IdUser!=emailValidate.IdUser)
            {
                throw new BusinessException("El Email ya se encuentra registrado");
            }
            //Validate if Dni was registered before
            var dniValidate = _unitOfWork.UserRepository.GetUserByDni(user.IdenDoc);
            if (dniValidate != null && user.IdUser != dniValidate.IdUser)
            {
                throw new BusinessException("El Dni ya se encuentra registrado");
            }
            //Obtenemos el usuario para guardar su contraseña
            var userValidate = await _unitOfWork.UserRepository.GetByIdTemp(user.IdUser);
            //Guardamos la contraseña nuevamente
            user.Password = userValidate.Password;
            foreach (var permission in user.UsersHasPermissions)
            {
                permission.Stardate = DateTime.Now;
                permission.Enddate = DateTime.Now.AddMonths(1);
                permission.CreatedAt = DateTime.Now;
                permission.UpdatedAt = DateTime.Now;
                permission.Username = email;
            }
            //Verificar que no se asigne el mismo permiso dos veces
            //Check that the same permission is not assigned twice
            foreach (UsersHasPermission usersHasPermission in user.UsersHasPermissions)
            {
                //Verificar que manden id de la relacion
                //Verify that they send the relationship id
                var idUserPermission = usersHasPermission.IdUserPermission;
                //Obtener el id del permiso
                //Get permission id
                UsersHasPermission permission = _unitOfWork.UsersHasPermissionRepository.GetUserPermission(user.IdUser, usersHasPermission.IdPermission);
                //si no mandan id de la relacion, verificar que se esté mandando el id de un permiso que no esta registrado
                //if they do not send the id of the relationship, verify that the id of a permit that is not registered is being sent
                if (idUserPermission == 0 && permission != null)
                {
                    throw new BusinessException("El permiso ya se encuentra asignado");
                }
            }
            //
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            //Eliminar permisos adicionales con status falso
            //Remove additional permissions with false status
            foreach (UsersHasPermission usersHasPermission in user.UsersHasPermissions)
            {
                if (usersHasPermission.Status == false)
                {
                    await _unitOfWork.UsersHasPermissionRepository.Delete(usersHasPermission.IdUserPermission);
                }
            }
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<bool> DeleteUser(int id)
        {
            //eliminacion lógica
            //var user =await _unitOfWork.UserRepository.GetById(id);
            //user.Status = false;
            //_unitOfWork.UserRepository.Update(user);
            //await _unitOfWork.SaveChangesAsync();
            //var roleHasPermisions = _unitOfWork.UsersHasPermissionRepository.GetAll().Where(x => x.IdUser== id);
            //foreach (var roleHasPermision in roleHasPermisions)
            //{
            //    roleHasPermision.Status = false;
            //    _unitOfWork.UsersHasPermissionRepository.Update(roleHasPermision);
            //}
            //await _unitOfWork.SaveChangesAsync();

            //eliminacion física
            _unitOfWork.UsersHasPermissionRepository.DeleteByUser(id);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.UserRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public PagedList<User> GetUsersByCompany(UserQueryFilter filters, int idCompany)
        {

            filters.Page = filters.Page == 0 ? _paginationOptions.DefaultPageNumber : filters.Page;
            filters.Size = filters.Size == 0 ? _paginationOptions.DefaultPageSize : filters.Size;

            var users = _unitOfWork.UserRepository.GetUsersByCompany(idCompany);
            if (filters.Name != null)
            {
                users = users.Where(x => x.Name.ToLower().Contains(filters.Name.ToLower()));
            }
            if (filters.Sort != null && filters.Order != null)
            {
                switch (filters.Sort.ToLower())
                {
                    case "name":
                        if (filters.Order == "asc") users = users.OrderBy(x => x.Name); else users = users.OrderByDescending(x => x.Name);
                        break;
                    case "tradename":
                        if (filters.Order == "asc") users = users.OrderBy(x => x.IdRolNavigation.IdCompanyNavigation.TradeName); else users = users.OrderByDescending(x => x.IdRolNavigation.IdCompanyNavigation.TradeName);
                        break;
                    case "email":
                        if (filters.Order == "asc") users = users.OrderBy(x => x.Email); else users = users.OrderByDescending(x => x.Email);
                        break;
                    default:
                        break;
                }
            }
            var pagedusers = PagedList<User>.Create(users, filters.Page, filters.Size);
            return pagedusers;
        }

        public async Task<User> GetLogiByCredentials(UserLogin User)
        { 
            User.Password = GetSHA256(User.Password);
            var user = await _unitOfWork.UserRepository.GetLoginByCredentials(User);
            return user;
        }
        public async Task<User> GetAditionalPermissions(int idUser)
        {
            var user = await _unitOfWork.UserRepository.GetAditionalPermissions(idUser);
            return user;
        }
        public User GetUserByEmail(String userName)
        {
            return _unitOfWork.UserRepository.GetUserByEmail(userName);
        }

        public bool UserPermissionValidation(int idUser, int idRol, String permission)
        {
            if (_unitOfWork.RoleHasPermissionRepository.RolePermissionValidation(idRol, permission) == true)
                return true;
            else if (_unitOfWork.UsersHasPermissionRepository.UserPermissionValidation(idUser, permission) == true)
                return true;
            else return false;
        }


        public bool RolePermissionValidation(int idRol, String permission)
        {
            if (_unitOfWork.RoleHasPermissionRepository.RolePermissionValidation(idRol, permission) == true)
                return true;
            else return false;
        }

        public bool UserPermissionValidation(int idUser, String permission)
        {
            //var bandera = _unitOfWork.UsersHasPermissionRepository.UserPermissionValidation(idUser, permission);
            if (_unitOfWork.UsersHasPermissionRepository.UserPermissionValidation(idUser, permission) == true)
                return true;
            else return false;
        }

        public string GetSHA256(String password)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new();
            StringBuilder sb = new();
            byte[] stream = sha256.ComputeHash(encoding.GetBytes(password));
            for (int i = 0; i < stream.Length; i++)
            {
                sb.AppendFormat("{0:x2}", stream[i]);
            }
            return sb.ToString();
        }

        public User GetUserDetailById(int idUser)
        {
            return _unitOfWork.UserRepository.GetUserDetailById(idUser);
        }

        public async Task ResetPassword(User user)
        {
            //Validate if company Id was registered before
            //var idCompanyValidate = await _unitOfWork.CompanyRepository.GetById(user.IdCompany);

            //if (idCompanyValidate == null)
            //{
            //    throw new BusinessException("La compañía no se encuentra registrada");
            //}
            //Validate if Email was registered before
            var emailValidate = _unitOfWork.UserRepository.GetUserByEmail(user.Email);
            if (emailValidate != null)
            {
                throw new BusinessException("El Email ya se encuentra registrado");
            }
            //Validate if Dni was registered before
            var dniValidate = _unitOfWork.UserRepository.GetUserByDni(user.IdenDoc);
            if (dniValidate != null)
            {
                throw new BusinessException("El Dni ya se encuentra registrado");
            }

            user.Password = GetSHA256(user.Password);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
