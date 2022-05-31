using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class VulAssementDetailHasAsset
    {
        public int IdVulnerabilityAssessmentDetail { get; set; }
        public int IdAsset { get; set; }
        public bool? Status { get; set; }

        public virtual Asset? IdAssetNavigation { get; set; }
        public virtual VulnerabilityAssessmentDetail? IdVulnerabilityAssessmentDetailNavigation { get; set; }
    }
}
