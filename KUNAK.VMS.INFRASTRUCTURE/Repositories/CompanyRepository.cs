using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.INFRASTRUCTURE.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(VMSContext context) : base(context) { }
        public IEnumerable<Company> GetCompanies()
        {
            //return _entities.Include(x => x.CompanyHasFrameworks).AsEnumerable();
            return _entities.AsEnumerable();
        }
        
        public async Task<Company> GetByRuc(string ruc)
        {
            return await _entities.AsNoTracking().FirstOrDefaultAsync(x => x.Ruc == ruc);
        }

        public async Task<Company> GetByEmail(string email)
        {
            return await _entities.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        }

        public Company GetByIdSync(int idCompany)
        {
            //return _entities.AsNoTracking().Include(x => x.CompanyHasFrameworks).Where(x => x.IdCompany == idCompany).FirstOrDefault();
            return _entities.AsNoTracking().Where(x => x.IdCompany == idCompany).FirstOrDefault();
        }
        //REVISAR
        public Company GetByRucSync(string ruc)
        {
            return _entities.AsNoTracking().FirstOrDefault(x => x.Ruc == ruc);
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return _entities.AsEnumerable();
        }
    }
}
