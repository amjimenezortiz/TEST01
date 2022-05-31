using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class Asset:BaseEntity
    {
        public Asset()
        {
            VulAssementDetailHasAssets = new HashSet<VulAssementDetailHasAsset>();
        }

        public int IdAsset { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }
        public int IdAssetType { get; set; }
        public int IdCriticality { get; set; }

        public virtual AssetType? IdAssetTypeNavigation { get; set; }
        public virtual Criticality? IdCriticalityNavigation { get; set; }
        public virtual ICollection<VulAssementDetailHasAsset> VulAssementDetailHasAssets { get; set; }
    }
}
