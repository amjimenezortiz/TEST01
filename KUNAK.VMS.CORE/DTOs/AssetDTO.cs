using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.DTOs
{
    public class AssetDTO
    {
        public int IdAsset { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }
        public int IdAssetType { get; set; }
        public int IdCriticality { get; set; }
    
    }
}
