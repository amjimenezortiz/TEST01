using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class ScopeDetail:BaseEntity
    {
        public ScopeDetail()
        {
            VulnerabilityAssessmentDetails = new HashSet<VulnerabilityAssessmentDetail>();
        }

        public int IdScopeDetail { get; set; }
        public int IdScope { get; set; }
        public string? Name { get; set; } 
        public string? ScopeDetailHtml { get; set; }
        public bool? Status { get; set; }


        public virtual Scope? IdScopeNavigation { get; set; }
        public virtual ICollection<VulnerabilityAssessmentDetail> VulnerabilityAssessmentDetails { get; set; }
    }
}
