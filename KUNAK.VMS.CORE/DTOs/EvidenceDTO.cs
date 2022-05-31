using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.DTOs
{
    public class EvidenceDTO
    {
        public int IdEvidence { get; set; }
        public string Filename { get; set; } = null!;
        public DateTime Date { get; set; }
        public string? FileExtension { get; set; }
        public int IdVulnerabilityAssessment { get; set; }
    }
}
