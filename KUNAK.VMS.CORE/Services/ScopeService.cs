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
    public class ScopeService:IScopeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ScopeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Scope> GetScopes(ScopeQueryFilter filters)
        {
            var scopes = _unitOfWork.ScopeRepository.GetAll();

            return scopes;
        }
        public Task<Scope> GetScope(int id)
        {
            return _unitOfWork.ScopeRepository.GetById(id);
        }
        public async Task InsertScope(Scope scope)
        {
            ////Validate if company RUC was registered before
            //var companyValidate = await _unitOfWork.CompanyRepository.GetByRuc(company.Ruc);
            //if (companyValidate != null)
            //{
            //    throw new BusinessException("El RUC ya se encuentra registrado");
            //}
            await _unitOfWork.ScopeRepository.Add(scope);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateScope(Scope scope)
        {
            ////Validate if company RUC was registered before
            //var companyValidate = await _unitOfWork.CompanyRepository.GetByRuc(company.Ruc);
            //// Se debe verificar que si es la misma empresa, debe permitir ingreasr el mismo ruc
            //// It must be verified that if it is the same company, it must allow entering the same ruc
            //if (companyValidate != null && company.IdCompany != companyValidate.IdCompany)
            //{
            //    throw new BusinessException("El RUC ya se encuentra registrado");
            //}
            _unitOfWork.ScopeRepository.Update(scope);
            await _unitOfWork.SaveChangesAsync();
            //----------------------
        }
        public async Task<bool> DeleteScope(int id)
        {
            await _unitOfWork.ScopeRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        //-----------------------------
        public bool ScopeIsRemoved(int idScope)
        {
            var scope = _unitOfWork.ScopeRepository.GetScopeDetails(idScope);
            var flag = true;
            //Verificamos si algún detalle evita eliminar al scope  
            foreach (var scopeDetail in scope.ScopeDetails)
            {
                var vulnerabilityDetails = _unitOfWork.VulnerabilityAssessmentDetailRepository
                    .GetByIdScopeDetail(scopeDetail.IdScopeDetail);
                if (vulnerabilityDetails.Any())
                {
                    flag = false;
                    break;
                }
            }
            //---------- 
            return flag;
            
        }
        public IEnumerable<Scope> GetScopesByIdVulnerabilityAssessment(int idVulnerabilityAssessment)
        {
            var scopes = _unitOfWork.ScopeRepository.GetScopesByIdVulnerabilityAssessment(idVulnerabilityAssessment);
            return scopes;
        }
    }
}
