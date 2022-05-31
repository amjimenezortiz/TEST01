using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.INFRASTRUCTURE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.INFRASTRUCTURE.Repositories
{
    public class ScopeDetailRepository:BaseRepository<ScopeDetail>, IScopeDetailRepository
    {
        public ScopeDetailRepository(VMSContext context) : base(context) { }
        public void DeleteByIdScope(int idScope)
        {
            _entities.RemoveRange(_entities.Where(x => x.IdScope == idScope));
        }
    }
}
