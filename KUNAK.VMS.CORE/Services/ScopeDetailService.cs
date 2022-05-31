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
    public class ScopeDetailService : IScopeDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ScopeDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<ScopeDetail> GetScopeDetails(ScopeDetailQueryFilter filters)
        {
            var scopeDetails = _unitOfWork.ScopeDetailRepository.GetAll();

            return scopeDetails;
        }
        public Task<ScopeDetail> GetScopeDetail(int id)
        {
            return _unitOfWork.ScopeDetailRepository.GetById(id);
        }
        public async Task InsertScopeDetail(ScopeDetail scopeDetail)
        {
            ////Validate if company RUC was registered before
            //var companyValidate = await _unitOfWork.CompanyRepository.GetByRuc(company.Ruc);
            //if (companyValidate != null)
            //{
            //    throw new BusinessException("El RUC ya se encuentra registrado");
            //}
            await _unitOfWork.ScopeDetailRepository.Add(scopeDetail);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateScopeDetail(ScopeDetail scopeDetail)
        {
            ////Validate if company RUC was registered before
            //var companyValidate = await _unitOfWork.CompanyRepository.GetByRuc(company.Ruc);
            //// Se debe verificar que si es la misma empresa, debe permitir ingreasr el mismo ruc
            //// It must be verified that if it is the same company, it must allow entering the same ruc
            //if (companyValidate != null && company.IdCompany != companyValidate.IdCompany)
            //{
            //    throw new BusinessException("El RUC ya se encuentra registrado");
            //}
            _unitOfWork.ScopeDetailRepository.Update(scopeDetail);
            await _unitOfWork.SaveChangesAsync();
            //----------------------
        }
        public async Task<bool> DeleteScopeDetail(int id)
        {
            await _unitOfWork.ScopeDetailRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public bool ScopeDetailIsRemoved(int idScopeDetail)
        {
            var vulnerabilityDetails = _unitOfWork.VulnerabilityAssessmentDetailRepository
                            .GetByIdScopeDetail(idScopeDetail);
            if (vulnerabilityDetails.Any())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
