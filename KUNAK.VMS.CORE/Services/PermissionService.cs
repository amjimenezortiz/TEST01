using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public IEnumerable<Permission> GetPermissions(PermissionQueryFilter filters)
        {
            var permissions = _unitOfWork.PermissionRepository.GetAll();
            if (filters.Name != null)
            {
                permissions = permissions.Where(x => x.Name.ToLower().Contains(filters.Name.ToLower()));
            }
            permissions = permissions.OrderBy(x => x.Name);

            return permissions;
        }
        public IEnumerable<Permission> GetPermissionsForClient(PermissionQueryFilter filters)
        {
            var permissions = _unitOfWork.PermissionRepository.GetPermissionsForClient();
            if (filters.Name != null)
            {
                permissions = permissions.Where(x => x.Name.ToLower().Contains(filters.Name.ToLower()));
            }
            permissions = permissions.OrderBy(x => x.Name);

            return permissions;
        }


        public async Task<Permission> GetPermission(int id)
        {
            return await _unitOfWork.PermissionRepository.GetById(id);
        }

        public async Task InsertPermission(Permission permission)
        {

            await _unitOfWork.PermissionRepository.Add(permission);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdatePermission(Permission permission)
        {

            _unitOfWork.PermissionRepository.Update(permission);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<bool> DeletePermission(int id)
        {
            await _unitOfWork.PermissionRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public IEnumerable<Permission> GetPermissionsAsync()
        {
            return _unitOfWork.PermissionRepository.GetPermissionsAsync();
        }

        public IEnumerable<Permission> GetPermissionsAditional(int idUser)
        {
            var permissions = _unitOfWork.PermissionRepository.GetAll().ToList();
            User user = _unitOfWork.UserRepository.GetByIdUser(idUser);
            var roleHasPermissions = _unitOfWork.RoleHasPermissionRepository.GetPermissionsByRol(user.IdRol).ToList();
            List<Permission> permissionsAditional = new();
            //Verifico que los permisos no estén asignados a ese rol
            //en caso lo estén flag toma el valor true, y solo se asignará ese permiso a la lista permissionsAditional
            //en caso flag sea false
            /*Verify that permissions are not assigned to that role. If they are, flag takes the value true, 
             * and that permission will only be assigned to the permissionsAdditional list if flag is false.*/
            foreach (var permission in permissions)
            {
                bool flag = false;
                foreach (var roleHasPermission in roleHasPermissions)
                {
                    if (permission.IdPermission == roleHasPermission.IdPermission)
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    permissionsAditional.Add(permission);
                }
            }
            //Verificar si el usuario al que se le desea añadir permisos es kunak, en ese caso se le puede mostrar todos;
            //en caso no sea kunak, solo se le va a mostrar lo permisos que sean para cliente
            /*Check if the user to whom you want to add permissions is kunak, in that case you can show them all; 
             * in case it is not kunak, it will only show you the permissions that are for the client*/
            if (user.IdRol == 1)
            {
                return permissionsAditional;
            }
            else
            {
                return permissionsAditional.Where(x => x.ForClient == true);
            }
        }
    }
}
