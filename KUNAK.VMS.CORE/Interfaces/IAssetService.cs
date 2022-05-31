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
    public interface IAssetService
    {
        IEnumerable<Asset> GetAssets(AssetQueryFilter filters);
        Task<Asset> GetAsset(int id);
        Task InsertAsset(Asset asset);
        Task UpdateAsset(Asset asset);
        Task<bool> DeleteAsset(int id);
    }
}
