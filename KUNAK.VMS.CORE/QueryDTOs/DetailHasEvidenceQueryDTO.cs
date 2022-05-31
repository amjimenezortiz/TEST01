using KUNAK.VMS.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.QueryDTOs
{
    public class DetailHasEvidenceQueryDTO
    {
        public string? Description { get; set; }
        public bool Status { get; set; }
        public int IdVulnerabilityAssessmentDetail { get; set; }
        public int IdEvidence { get; set; }
        public virtual EvidenceDTO Evidence { get; set; } = null!;
    }
}
