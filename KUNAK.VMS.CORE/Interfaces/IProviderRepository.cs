using KUNAK.VMS.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IProviderRepository : IRepository<Provider>
    {
        Task<Provider> GetByRuc(string providerStatus);
        Task<Provider> GetByEmail(string email);
        IEnumerable<Provider> GetProvidersByCompany(int idCompany);

        IEnumerable<Provider> GetProvidersCompany();
    }
}
