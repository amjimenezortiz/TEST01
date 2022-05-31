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
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public IEnumerable<Role> GetAllRoles(RoleQueryFilter filters)
        {

            var roles = _unitOfWork.RoleRepository.GetAllRoles();
            if (filters.Name != null)
            {
                roles = roles.Where(x => x.Name.ToLower().Contains(filters.Name.ToLower()));
            }
            roles = roles.OrderBy(x => x.Name);
            return roles;
        }


        public async Task<Role> GetRole(int id)
        {
            return await _unitOfWork.RoleRepository.GetRole(id);
        }

        public async Task InsertRole(Role role)
        {
            //Validate if company Id was registered before
            var idCompanyValidate = await _unitOfWork.CompanyRepository.GetById(role.IdCompany);

            if (idCompanyValidate == null)
            {
                throw new BusinessException("La compañía no se encuentra registrada");
            }

            await _unitOfWork.RoleRepository.Add(role);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateRole(Role role)
        {
            //Validate if company Id was registered before
            var idCompanyValidate = await _unitOfWork.CompanyRepository.GetById(role.IdCompany);

            if (idCompanyValidate == null)
            {
                throw new BusinessException("La compañía no se encuentra registrada");
            }
            _unitOfWork.RoleRepository.Update(role);
            await _unitOfWork.SaveChangesAsync();
            //----------------------------------
        }

        public async Task<bool> DeleteRole(int id)
        {
            //Verificar que el rol no este asignado a ningun usuario
            if (_unitOfWork.UserRepository.GetByRol(id))
            {
                throw new BusinessException("El rol esta asignado a un usuario");
            }

            _unitOfWork.RoleHasPermissionRepository.DeleteByRole(id);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.RoleRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public IEnumerable<Role> GetRolesByCompany(RoleQueryFilter filters, int idCompany)
        {
            var roles = _unitOfWork.RoleRepository.GetRolesByCompany(idCompany);
            if (filters.Name != null)
            {
                roles = roles.Where(x => x.Name.ToLower().Contains(filters.Name.ToLower()));
            }
            roles = roles.OrderBy(x => x.Name);
            return roles;
        }
    }
}
