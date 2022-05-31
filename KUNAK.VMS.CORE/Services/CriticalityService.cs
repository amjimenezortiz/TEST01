using KUNAK.VMS.CORE.CustomEntities;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.CORE.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Services
{
    public class CriticalityService : ICriticalityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public CriticalityService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public IEnumerable<Criticality> GetCriticalities(CriticalityQueryFilter filters)
        {
            
            var criticalities = _unitOfWork.CriticalityRepository.GetAll();
            //if (filters.Ruc != null)
            //{
            //    companies = companies.Where(x => x.Ruc.Contains(filters.Ruc));
            //}
            //if (filters.TradeName != null)
            //{
            //    companies = companies.Where(x => x.TradeName.ToLower().Contains(filters.TradeName.ToLower()));
            //}
            //companies = companies.OrderBy(x => x.TradeName);
            return criticalities;
        }
        public Task<Criticality> GetCriticality(int id)
        {
            return _unitOfWork.CriticalityRepository.GetById(id);
        }
        public async Task InsertCriticality(Criticality criticality)
        {
            ////Validate if company RUC was registered before
            //var companyValidate = await _unitOfWork.CompanyRepository.GetByRuc(company.Ruc);
            //if (companyValidate != null)
            //{
            //    throw new BusinessException("El RUC ya se encuentra registrado");
            //}
            await _unitOfWork.CriticalityRepository.Add(criticality);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateCriticality(Criticality criticality)
        {
            ////Validate if company RUC was registered before
            //var companyValidate = await _unitOfWork.CompanyRepository.GetByRuc(company.Ruc);
            //// Se debe verificar que si es la misma empresa, debe permitir ingreasr el mismo ruc
            //// It must be verified that if it is the same company, it must allow entering the same ruc
            //if (companyValidate != null && company.IdCompany != companyValidate.IdCompany)
            //{
            //    throw new BusinessException("El RUC ya se encuentra registrado");
            //}
            _unitOfWork.CriticalityRepository.Update(criticality);
            await _unitOfWork.SaveChangesAsync();
            //----------------------
        }
        public async Task<bool> DeleteCriticality(int id)
        {
            await _unitOfWork.CriticalityRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
