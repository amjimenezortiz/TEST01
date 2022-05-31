using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class Activity
    {
        public int IdTreatment { get; set; }
        public int IdActivity { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public int IdResponsablePerson { get; set; }
        public bool? Status { get; set; }
        public string? StatusDescription { get; set; }

        public virtual Treatment? IdTreatmentNavigation { get; set; }
    }
}
