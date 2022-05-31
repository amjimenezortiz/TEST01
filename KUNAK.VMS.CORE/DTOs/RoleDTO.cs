using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.DTOs
{
    public class RoleDTO
    {
        public int IdRol { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }
        public int IdCompany { get; set; }
    }
}
