using KUNAK.VMS.CORE.CustomEntities;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Exceptions;
using KUNAK.VMS.CORE.Interfaces;

using KUNAK.VMS.CORE.QueryFilters;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProviderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Provider> GetProviders(ProviderQueryFilter filters)
        {
            var providers = _unitOfWork.ProviderRepository.GetProvidersCompany();

            if (filters.Ruc != null)
            {
                providers = providers.Where(x => x.Ruc.Contains(filters.Ruc));
            }
            if (filters.EmployeeName != null)
            {
                providers = providers.Where(x => x.EmployeeName.ToLower().Contains(filters.EmployeeName.ToLower()));
            }

            providers = providers.OrderBy(x => x.EmployeeName);
            return providers;
        }


        public async Task<Provider> GetProvider(int id)
        {
            return await _unitOfWork.ProviderRepository.GetById(id);
        }

        public async Task InsertProvider(Provider provider)
        {
            //Validate if company Id was registered before
            var idCompanyValidate = await _unitOfWork.CompanyRepository.GetById(provider.IdCompany);
            if (idCompanyValidate == null)
            {
                throw new BusinessException("La compañía no se encuentra registrada");
            }
            var rucValidate = await _unitOfWork.ProviderRepository.GetByRuc(provider.Ruc);
            if (rucValidate != null)
            {
                throw new BusinessException("El RUC ya se encuentra registrado");
            }
            await _unitOfWork.ProviderRepository.Add(provider);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateProvider(Provider provider)
        {
            //Validate if company Id was registered before
            var idCompanyValidate = await _unitOfWork.CompanyRepository.GetById(provider.IdCompany);

            if (idCompanyValidate == null)
            {
                throw new BusinessException("La compañía no se encuentra registrada");
            }
            //Verificar que si es el mismo proveedor, debe permitir ingresar el mismo ruc
            //Verify that if it is the same provider, it must allow entering the same ruc
            var rucValidate = await _unitOfWork.ProviderRepository.GetByRuc(provider.Ruc);
            if (rucValidate != null && provider.IdProvider != rucValidate.IdProvider)
            {
                throw new BusinessException("El RUC ya se encuentra registrado");
            }
            _unitOfWork.ProviderRepository.Update(provider);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<bool> DeleteProvider(int id, int idCompany)
        {
            //Eliminar provedor si no tiene Gaps relacionados
            //Delete provider if it does not have related Gaps
            //var gapAnalyzes = _unitOfWork.GapAnalyzeRepository.GetGapAnalyzesDetail(idCompany).Where(x => x.IdProviders == id);
            //if (!gapAnalyzes.Any())
            //{
                Provider provider = await _unitOfWork.ProviderRepository.GetById(id);
                provider.Status = false;
                await UpdateProvider(provider);
                await _unitOfWork.SaveChangesAsync();
                return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        public IEnumerable<Provider> GetProvidersByCompany(ProviderQueryFilter filters, int idCompany)
        {
            var providers = _unitOfWork.ProviderRepository.GetProvidersByCompany(idCompany);
            if (filters.Ruc != null)
            {
                providers = providers.Where(x => x.Ruc.Contains(filters.Ruc));
            }
            if (filters.EmployeeName != null)
            {
                providers = providers.Where(x => x.EmployeeName.ToLower().Contains(filters.EmployeeName.ToLower()));
            }
            providers = providers.OrderBy(x => x.EmployeeName);
            return providers;
        }
    }
}
