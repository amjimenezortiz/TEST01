using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class TreatmentHasState
    {
        public int IdTreatmentStates { get; set; }
        public int IdTreatment { get; set; }
        public int IdTreatmentHasStates { get; set; }
        public DateTime Date { get; set; }
        public int IdResponsablePerson { get; set; }
        public bool? Status { get; set; }

        public virtual Treatment? IdTreatmentNavigation { get; set; }
        public virtual TreatmentState? IdTreatmentStatesNavigation { get; set; }
    }
}
