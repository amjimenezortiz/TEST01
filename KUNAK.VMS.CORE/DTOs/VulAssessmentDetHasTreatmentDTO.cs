using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.DTOs
{
    public class VulAssessmentDetHasTreatmentDTO
    {
        public int IdVulnerabilityAssessmentDetail { get; set; }
        public int IdTreatment { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
    }
}
