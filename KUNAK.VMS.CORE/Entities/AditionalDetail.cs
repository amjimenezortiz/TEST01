using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class AditionalDetail:BaseEntity
    {
        public int IdAditionalDetail { get; set; }
        public int IdVulnerabilityAssessmentDetail { get; set; }
        public string? Name { get; set; }
        public string? DescriptionHtml { get; set; }
        public bool? Status { get; set; }

        public virtual VulnerabilityAssessmentDetail? IdVulnerabilityAssessmentDetailNavigation { get; set; }
    }
}
