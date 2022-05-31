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
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Company> GetCompanies(CompanyQueryFilter filters)
        {
            var companies = _unitOfWork.CompanyRepository.GetCompanies();
            if (filters.Ruc != null)
            {
                companies = companies.Where(x => x.Ruc.Contains(filters.Ruc));
            }
            if (filters.TradeName != null)
            {
                companies = companies.Where(x => x.TradeName.ToLower().Contains(filters.TradeName.ToLower()));
            }
            companies = companies.OrderBy(x => x.TradeName);
            return companies;
        }
        public Company GetCompany(int id)
        {
            return _unitOfWork.CompanyRepository.GetByIdSync(id);
        }
        public async Task InsertCompany(Company company)
        {
            //Validate if company RUC was registered before
            var companyValidate = await _unitOfWork.CompanyRepository.GetByRuc(company.Ruc);
            if (companyValidate != null)
            {
                throw new BusinessException("El RUC ya se encuentra registrado");
            }
            await _unitOfWork.CompanyRepository.Add(company);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateCompany(Company company)
        {
            //Validate if company RUC was registered before
            var companyValidate = await _unitOfWork.CompanyRepository.GetByRuc(company.Ruc);
            // Se debe verificar que si es la misma empresa, debe permitir ingreasr el mismo ruc
            // It must be verified that if it is the same company, it must allow entering the same ruc
            if (companyValidate != null && company.IdCompany != companyValidate.IdCompany)
            {
                throw new BusinessException("El RUC ya se encuentra registrado");
            }
            _unitOfWork.CompanyRepository.Update(company);
            await _unitOfWork.SaveChangesAsync();
            //----------------------
        }
        public async Task<bool> DeleteCompany(int id)
        {
            await _unitOfWork.CompanyRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public Company GetByRucSync(string ruc)
        {
            return _unitOfWork.CompanyRepository.GetByRucSync(ruc);
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            var companies = _unitOfWork.CompanyRepository.GetAllCompanies();
            return companies;
        }
    }
}
