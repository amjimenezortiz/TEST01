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
    public class ScopeRepository: BaseRepository<Scope>,IScopeRepository
    {
        public ScopeRepository(VMSContext context) : base(context) { }
        public Scope GetScopeDetails(int idScope)
        {
            return _entities.Include(x => x.ScopeDetails)
                .Where(x => x.IdScope == idScope).FirstOrDefault();
        }
        public IEnumerable<Scope> GetScopesByIdVulnerabilityAssessment(int idVulnerabilityAssessment)
        {
            return _entities.Include(x => x.ScopeDetails)
                .Where(x => x.IdVulnerabilityAssessment == idVulnerabilityAssessment).AsEnumerable();
        }
        public void DeleteByIdVulnerabilityAssessment(int idVulnerabilityAssessment)
        {
            _entities
             .RemoveRange(_entities.Where(x => x.IdVulnerabilityAssessment == idVulnerabilityAssessment));
        }
    }
}
