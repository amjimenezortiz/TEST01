using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.INFRASTRUCTURE.Repositories
{
    public class ProviderRepository : BaseRepository<Provider>, IProviderRepository
    {
        public ProviderRepository(VMSContext context) : base(context) { }
        public async Task<Provider> GetByRuc(string ruc)
        {
            if(string.IsNullOrEmpty(ruc)) throw new ArgumentNullException(nameof(ruc));

            return await _entities.AsNoTracking().FirstOrDefaultAsync(x => x.Ruc == ruc);
        }

        public async Task<Provider> GetByEmail(string email)
        {
            return await _entities.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        }
        public IEnumerable<Provider> GetProvidersByCompany(int idCompany)
        {
            return _entities.AsEnumerable().Where(x => x.IdCompany == idCompany && x.Status == true);
        }

        public IEnumerable<Provider> GetProvidersCompany()
        {
            return _entities.Include(x => x.IdCompanyNavigation).Where(x => x.Status == true).AsEnumerable();
        }

    }
}
