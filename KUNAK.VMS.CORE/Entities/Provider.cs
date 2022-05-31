using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class Provider : BaseEntity
    {
        public Provider()
        {
            VulnerabilityAssessments = new HashSet<VulnerabilityAssessment>();
        }

        public int IdProvider { get; set; }
        public int IdCompany { get; set; }
        public string Ruc { get; set; } = null!;
        public string? CompanyName { get; set; } 
        public string EmployeeName { get; set; } = null!;
        public string? Phone { get; set; } 
        public string? Email { get; set; } 
        public bool? Status { get; set; }

        public virtual Company? IdCompanyNavigation { get; set; }
        public virtual ICollection<VulnerabilityAssessment> VulnerabilityAssessments { get; set; }
    }
}
