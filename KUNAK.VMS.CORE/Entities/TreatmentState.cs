using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class TreatmentState
    {
        public TreatmentState()
        {
            TreatmentHasStates = new HashSet<TreatmentHasState>();
        }

        public int IdTreatmentStates { get; set; }
        public string? Name { get; set; } 
        public string? Color { get; set; } 
        public int Order { get; set; }

        public virtual ICollection<TreatmentHasState> TreatmentHasStates { get; set; }
    }
}
