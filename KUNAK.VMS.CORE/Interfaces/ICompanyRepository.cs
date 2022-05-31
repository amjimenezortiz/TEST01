using KUNAK.VMS.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        //Here we can add additional methods
        IEnumerable<Company> GetCompanies();
        Task<Company> GetByRuc(string ruc);
        Task<Company> GetByEmail(string email);
        Company GetByIdSync(int idCompany);
        Company GetByRucSync(string ruc);
        IEnumerable<Company> GetAllCompanies();
    }
}
