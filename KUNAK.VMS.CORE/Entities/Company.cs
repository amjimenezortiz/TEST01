using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class Company : BaseEntity
    {
        public Company()
        {
            Areas = new HashSet<Area>();
            Providers = new HashSet<Provider>();
            Roles = new HashSet<Role>();
            VulnerabilityAssessments = new HashSet<VulnerabilityAssessment>();
        }

        public int IdCompany { get; set; }
        public string Ruc { get; set; } = null!;
        public string TradeName { get; set; } = null!;
        public string? Address { get; set; }
        public string? Region { get; set; }
        public string? Province { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool? Status { get; set; }
        public string? Logo { get; set; }
        public string? District { get; set; }

        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<Provider> Providers { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<VulnerabilityAssessment> VulnerabilityAssessments { get; set; }
    }
}
