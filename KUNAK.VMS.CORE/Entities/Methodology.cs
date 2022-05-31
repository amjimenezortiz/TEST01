using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class Methodology:BaseEntity
    {
        public Methodology()
        {
            VulnerabilityAssessmentHasMethodologies = new HashSet<VulnerabilityAssessmentHasMethodology>();
        }

        public int IdMethodologie { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool? Status { get; set; }

        public virtual ICollection<VulnerabilityAssessmentHasMethodology> VulnerabilityAssessmentHasMethodologies { get; set; }
    }
}
