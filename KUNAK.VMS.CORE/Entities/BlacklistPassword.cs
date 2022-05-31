using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class BlacklistPassword : BaseEntity
    {
        public int IdPassword { get; set; }
        public string? Description { get; set; }
    }
}
