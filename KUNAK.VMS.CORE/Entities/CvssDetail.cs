using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class CvssDetail
    {
        public int IdCvssDetail { get; set; }
        public int IdVulnerabilityAssessmentDetail { get; set; }
        public string? Vector { get; set; }
        public string? Compexity { get; set; }
        public string? PrivilegeLevel { get; set; }
        public string? UserInteration { get; set; }
        public string? Scope { get; set; }
        public string? Confidentiality { get; set; }
        public string? Integrity { get; set; }
        public string? Availability { get; set; }
        public string? ExploitAccess { get; set; }
        public string? VulnerabilityFix { get; set; }
        public string? ReliabilityLevel { get; set; }
        public double BaseScore { get; set; }
        public double ImpactSubscore { get; set; }
        public double ExploitabilitySubscore { get; set; }
        public double TemporaryPunctuation { get; set; }
        public double EnvironmentScore { get; set; }
        public double AverageScore { get; set; }

        public virtual VulnerabilityAssessmentDetail? IdVulnerabilityAssessmentDetailNavigation { get; set; }
    }
}
