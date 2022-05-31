using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.QueryFilters;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IProviderService
    {
        IEnumerable<Provider> GetProviders(ProviderQueryFilter filters);

        IEnumerable<Provider> GetProvidersByCompany(ProviderQueryFilter filters, int idCompany);
        Task<Provider> GetProvider(int id);
        Task InsertProvider(Provider provider);
        Task UpdateProvider(Provider provider);
        Task<bool> DeleteProvider(int id, int idCompany);
    }
}
