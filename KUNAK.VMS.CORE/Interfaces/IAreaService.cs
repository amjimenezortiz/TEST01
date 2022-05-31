using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IAreaService
    {
        IEnumerable<Area> GetAreas(AreaQueryFilter filters);
        Task<Area> GetArea(int id);
        Task InsertArea(Area area);
        Task UpdateArea(Area area);
        Task<bool> DeleteArea(int id);
    }
}
