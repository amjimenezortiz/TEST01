using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.DTOs
{
    public class AditionalDetailDTO
    {
        public int IdAditionalDetail { get; set; }
        public int IdVulnerabilityAssessmentDetail { get; set; }
        public string? Name { get; set; }
        public string? DescriptionHtml { get; set; }
        public bool? Status { get; set; }
    }
}
