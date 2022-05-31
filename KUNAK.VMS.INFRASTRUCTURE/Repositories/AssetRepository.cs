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
    public class AssetRepository:BaseRepository<Asset>, IAssetRepository
    {
        public AssetRepository(VMSContext context) : base(context) { }
    }
}
