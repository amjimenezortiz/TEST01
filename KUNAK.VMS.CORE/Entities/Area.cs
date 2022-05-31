using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class Area:BaseEntity
    {
        public Area()
        {
            People = new HashSet<Person>();
        }

        public int IdArea { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }
        public int IdCompany { get; set; }

        public virtual Company? IdCompanyNavigation { get; set; }
        public virtual ICollection<Person> People { get; set; }
    }
}
