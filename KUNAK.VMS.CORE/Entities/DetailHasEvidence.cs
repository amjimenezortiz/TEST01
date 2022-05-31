using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class DetailHasEvidence:BaseEntity
    {
        public bool? Status { get; set; }
        public int IdVulnerabilityAssessmentDetail { get; set; }
        public int IdEvidence { get; set; }

        public virtual Evidence IdEvidenceNavigation { get; set; } = null!;
        public virtual VulnerabilityAssessmentDetail? IdVulnerabilityAssessmentDetailNavigation { get; set; }
    }
}
