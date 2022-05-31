using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.DTOs
{
    public class ScopeDTO
    {
        public int IdScope { get; set; }
        public int IdVulnerabilityAssessment { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<ScopeDetailDTO>? ScopeDetails { get; set; }
        public bool? Status { get; set; }

    }
}
