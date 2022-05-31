using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.DTOs
{
    public class VulAssementDetailHasAssetDTO
    {
        public int IdVulnerabilityAssessmentDetail { get; set; }
        public int IdAsset { get; set; }
        public bool? Status { get; set; }
    }
}
