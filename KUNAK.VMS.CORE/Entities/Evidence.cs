using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class Evidence : BaseEntity
    {
        public Evidence()
        {
            DetailHasEvidences = new HashSet<DetailHasEvidence>();
        }

        public int IdEvidence { get; set; }
        public string Filename { get; set; } = null!;
        public DateTime Date { get; set; }
        public string FileExtension { get; set; } = null!;
        public int IdVulnerabilityAssessment { get; set; }

        public virtual ICollection<DetailHasEvidence> DetailHasEvidences { get; set; }
    }
}
