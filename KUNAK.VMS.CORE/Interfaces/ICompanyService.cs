using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface ICompanyService
    {
        IEnumerable<Company> GetCompanies(CompanyQueryFilter filters);
        Company GetCompany(int id);
        Task InsertCompany(Company company);
        Task UpdateCompany(Company company);
        Task<bool> DeleteCompany(int id);
        Company GetByRucSync(string ruc);
        IEnumerable<Company> GetAllCompanies();
    }
}
