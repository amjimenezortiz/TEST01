using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class Person : BaseEntity
    {
        public int IdPerson { get; set; }
        public string? LastName { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int IdArea { get; set; }
        public string? EmployeeCode { get; set; }

        public virtual Area? IdAreaNavigation { get; set; }
    }
}
