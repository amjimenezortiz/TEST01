﻿using KUNAK.VMS.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IScopeDetailRepository:IRepository<ScopeDetail>
    {
        void DeleteByIdScope(int idScope);
    }
}
