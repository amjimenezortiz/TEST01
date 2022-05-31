using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IScopeDetailService
    {
        IEnumerable<ScopeDetail> GetScopeDetails(ScopeDetailQueryFilter filters);
        Task<ScopeDetail> GetScopeDetail(int id);
        Task InsertScopeDetail(ScopeDetail scopeDetail);
        Task UpdateScopeDetail(ScopeDetail scopeDetail);
        Task<bool> DeleteScopeDetail(int id);
        //------------------
        bool ScopeDetailIsRemoved(int idScopeDetail);
    }
}
