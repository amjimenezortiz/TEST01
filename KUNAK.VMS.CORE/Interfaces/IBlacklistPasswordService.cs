using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IBlacklistPasswordService
    {
        IEnumerable<BlacklistPassword> GetBlacklistPasswords(BlacklistPasswordQueryFilter filters);
        Task<BlacklistPassword> GetBlacklistPassword(int id);
        Task InsertBlacklistPassword(BlacklistPassword blacklistPassword);
        Task UpdateBlacklistPassword(BlacklistPassword blacklistPassword);
        Task<bool> DeleteBlacklistPassword(int id);
    }
}
