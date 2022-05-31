using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IScopeService
    {
        IEnumerable<Scope> GetScopes(ScopeQueryFilter filters);
        Task<Scope> GetScope(int id);
        Task InsertScope(Scope scope);
        Task UpdateScope(Scope scope);
        Task<bool> DeleteScope(int id);
        //-----------
        bool ScopeIsRemoved(int idScope);
        IEnumerable<Scope> GetScopesByIdVulnerabilityAssessment(int idVulnerabilityAssessment);
    }
}
