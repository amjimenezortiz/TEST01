using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.DTOs
{
    public class TreatmentDTO
    {
        public int IdTreatment { get; set; }
        public int Order { get; set; }
        public int IdSupervisorPerson { get; set; }
        public int IdResponsablePerson { get; set; }
        public int IdSupervisorArea { get; set; }
        public int IdResponsableArea { get; set; }
        public DateTime ProposedEndDate { get; set; }
        public DateTime MaximalEndDate { get; set; }
        public DateTime RealEndDate { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Cost { get; set; }
        public string? Currency { get; set; }
        public string? StatusDescription { get; set; }
    }
}
