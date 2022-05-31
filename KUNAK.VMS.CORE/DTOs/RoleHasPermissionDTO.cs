using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.DTOs
{
    public class RoleHasPermissionDTO
    {
        public bool? Status { get; set; }
        public int IdPermission { get; set; }
        public int IdRol { get; set; }
    }
}
