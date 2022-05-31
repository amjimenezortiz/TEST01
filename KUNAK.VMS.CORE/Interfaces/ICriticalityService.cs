using KUNAK.VMS.CORE.CustomEntities;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface ICriticalityService
    {
        IEnumerable<Criticality> GetCriticalities(CriticalityQueryFilter filters);
        Task<Criticality> GetCriticality(int id);
        Task InsertCriticality(Criticality criticality);
        Task UpdateCriticality(Criticality criticality);
        Task<bool> DeleteCriticality(int id);
    }
}
