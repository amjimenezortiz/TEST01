using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class AssetType:BaseEntity
    {
        public AssetType()
        {
            Assets = new HashSet<Asset>();
        }

        public int IdAssetType { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}
