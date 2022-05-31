using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IMethodologyService
    {
        IEnumerable<Methodology> GetMethodologys(MethodologyQueryFilter filters);
        Task<Methodology> GetMethodology(int id);
        Task InsertMethodology(Methodology methodology);
        Task UpdateMethodology(Methodology methodology);
        Task<bool> DeleteMethodology(int id);
    }
}
