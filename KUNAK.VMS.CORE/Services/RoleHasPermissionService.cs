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
    public class RoleHasPermissionService : IRoleHasPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleHasPermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        //Front said that they don't need pagination for this CRUD
        public IEnumerable<RoleHasPermission> GetRoleHasPermissions(RoleHasPermissionQueryFilter filters)
        {
            var RoleHasPermissions = _unitOfWork.RoleHasPermissionRepository.GetAll();
            return RoleHasPermissions;
        }
        public RoleHasPermission GetRoleHasPermission(int idRol, int idPermission)
        {
            return _unitOfWork.RoleHasPermissionRepository.GetByIdConcatenated(idRol, idPermission);
        }
        public async Task InsertRoleHasPermission(RoleHasPermission roleHasPermission)
        {
            //Validate if Role id and Permission id was registered before

            var idRolValidate = await _unitOfWork.RoleRepository.GetById(roleHasPermission.IdRol);

            if (idRolValidate == null)
            {
                throw new BusinessException("El rol no se encuentra registrado");
            }
            var idPermissionValidate = await _unitOfWork.PermissionRepository.GetById(roleHasPermission.IdPermission);

            if (idPermissionValidate == null)
            {
                throw new BusinessException("El permiso no se encuentra registrado");
            }

            await _unitOfWork.RoleHasPermissionRepository.Add(roleHasPermission);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateRoleHasPermission(RoleHasPermission roleHasPermission)
        {
            //Validate if Role id and Permission id was registered before

            var idRolValidate = await _unitOfWork.RoleRepository.GetById(roleHasPermission.IdRol);

            if (idRolValidate == null)
            {
                throw new BusinessException("El rol no se encuentra registrado");
            }
            var idPermissionValidate = await _unitOfWork.PermissionRepository.GetById(roleHasPermission.IdPermission);

            if (idPermissionValidate == null)
            {
                throw new BusinessException("El permiso no se encuentra registrado");
            }
            _unitOfWork.RoleHasPermissionRepository.Update(roleHasPermission);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteRoleHasPermission(int idRol, int idPermission)
        {
            _unitOfWork.RoleHasPermissionRepository.DeleteByIdConcatenated(idRol, idPermission);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task UpdatePermissionByRol(int idRol, RoleHasPermission roleHasPermission)
        {

            if (_unitOfWork.RoleHasPermissionRepository.GetByIdTemp(idRol, roleHasPermission.IdPermission) != null)
            {
                roleHasPermission.IdRol = idRol;

                _unitOfWork.RoleHasPermissionRepository.Update(roleHasPermission);
            }
            else
            {
                roleHasPermission.IdRol = idRol;
                await _unitOfWork.RoleHasPermissionRepository.Add(roleHasPermission);
            }
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
