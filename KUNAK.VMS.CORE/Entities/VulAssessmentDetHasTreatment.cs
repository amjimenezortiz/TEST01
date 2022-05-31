using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class VulAssessmentDetHasTreatment
    {
        public int IdVulnerabilityAssessmentDetail { get; set; }
        public int IdTreatment { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }

        public virtual Treatment? IdTreatmentNavigation { get; set; }
        public virtual VulnerabilityAssessmentDetail? IdVulnerabilityAssessmentDetailNavigation { get; set; }
    }
}
