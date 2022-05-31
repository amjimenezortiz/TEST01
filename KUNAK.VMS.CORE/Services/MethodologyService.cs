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
    public class MethodologyService : IMethodologyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MethodologyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Methodology> GetMethodologys(MethodologyQueryFilter filters)
        {
            var methodologys = _unitOfWork.MethodologyRepository.GetAll();

            return methodologys;
        }
        public Task<Methodology> GetMethodology(int id)
        {
            return _unitOfWork.MethodologyRepository.GetById(id);
        }
        public async Task InsertMethodology(Methodology methodology)
        {
            ////Validate if company RUC was registered before
            //var companyValidate = await _unitOfWork.CompanyRepository.GetByRuc(company.Ruc);
            //if (companyValidate != null)
            //{
            //    throw new BusinessException("El RUC ya se encuentra registrado");
            //}
            await _unitOfWork.MethodologyRepository.Add(methodology);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateMethodology(Methodology methodology)
        {
            ////Validate if company RUC was registered before
            //var companyValidate = await _unitOfWork.CompanyRepository.GetByRuc(company.Ruc);
            //// Se debe verificar que si es la misma empresa, debe permitir ingreasr el mismo ruc
            //// It must be verified that if it is the same company, it must allow entering the same ruc
            //if (companyValidate != null && company.IdCompany != companyValidate.IdCompany)
            //{
            //    throw new BusinessException("El RUC ya se encuentra registrado");
            //}
            _unitOfWork.MethodologyRepository.Update(methodology);
            await _unitOfWork.SaveChangesAsync();
            //----------------------
        }
        public async Task<bool> DeleteMethodology(int id)
        {
            await _unitOfWork.MethodologyRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
