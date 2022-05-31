using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.DTOs
{
    public class TreatmentHasStateDTO
    {
        public int IdTreatmentStates { get; set; }
        public int IdTreatment { get; set; }
        public int IdTreatmentHasStates { get; set; }
        public DateTime Date { get; set; }
        public int IdResponsablePerson { get; set; }
        public bool? Status { get; set; }
    }
}
