using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class TreatmentEvidence
    {
        public int IdTreatmentEvidences { get; set; }
        public int IdTreatment { get; set; }
        public string? Filename { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public bool? Status { get; set; }

        public virtual Treatment? IdTreatmentNavigation { get; set; }
    }
}
