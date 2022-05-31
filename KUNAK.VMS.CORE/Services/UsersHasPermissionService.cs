using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Exceptions;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Services
{
    public class UsersHasPermissionService : IUsersHasPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersHasPermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public IEnumerable<UsersHasPermission> GetUsersHasPermissions(UsersHasPermissionQueryFilter filters)
        {
            var usersHasPermissions = _unitOfWork.UsersHasPermissionRepository.GetAll();
            return usersHasPermissions;
        }

        public async Task<UsersHasPermission> GetUsersHasPermission(int id)
        {
            return await _unitOfWork.UsersHasPermissionRepository.GetById(id);
        }

        public async Task InsertUsersHasPermission(UsersHasPermission usersHasPermission)
        {
            //Validate if idUser and idPermission was registered before
            var idUserValidate = await _unitOfWork.UserRepository.GetById(usersHasPermission.IdUser);

            if (idUserValidate == null)
            {
                throw new BusinessException("El usuario no se encuentra registrado");
            }
            var idPermissionValidate = await _unitOfWork.PermissionRepository.GetById(usersHasPermission.IdPermission);
            if (idPermissionValidate == null)
            {
                throw new BusinessException("El permiso no se encuentra registrado");
            }
            await _unitOfWork.UsersHasPermissionRepository.Add(usersHasPermission);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateUsersHasPermission(UsersHasPermission usersHasPermission)
        {
            //Validate if idUser and idPermission was registered before
            var idUserValidate = await _unitOfWork.UserRepository.GetById(usersHasPermission.IdUser);

            if (idUserValidate == null)
            {
                throw new BusinessException("El usuario no se encuentra registrado");
            }
            var idPermissionValidate = await _unitOfWork.PermissionRepository.GetById(usersHasPermission.IdPermission);

            if (idPermissionValidate == null)
            {
                throw new BusinessException("El permiso no se encuentra registrado");
            }
            _unitOfWork.UsersHasPermissionRepository.Update(usersHasPermission);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<bool> DeleteUsersHasPermission(int id)
        {
            await _unitOfWork.UsersHasPermissionRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
