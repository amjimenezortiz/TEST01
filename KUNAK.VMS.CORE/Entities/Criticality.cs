using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class Criticality : BaseEntity
    {
        public Criticality()
        {
            Assets = new HashSet<Asset>();
        }

        public int IdCriticality { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}
