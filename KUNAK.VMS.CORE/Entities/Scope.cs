using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class Scope:BaseEntity
    {
        public Scope()
        {
            ScopeDetails = new HashSet<ScopeDetail>();
        }

        public int IdScope { get; set; }
        public int IdVulnerabilityAssessment { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }


        public virtual VulnerabilityAssessment? IdVulnerabilityAssessmentNavigation { get; set; }
        public virtual ICollection<ScopeDetail> ScopeDetails { get; set; }
    }
}
