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
    public interface IAssetTypeService
    {
        IEnumerable<AssetType> GetAssetTypes(AssetTypeQueryFilter filters);
        Task<AssetType> GetAssetType(int id);
        Task InsertAssetType(AssetType assetType);
        Task UpdateAssetType(AssetType assetType);
        Task<bool> DeleteAssetType(int id);
    }
}
