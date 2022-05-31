using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.DTOs
{
    public class DetailHasEvidenceDTO
    {
        public bool? Status { get; set; }
        public int IdVulnerabilityAssessmentDetail { get; set; }
        public int IdEvidence { get; set; }
    }
}
