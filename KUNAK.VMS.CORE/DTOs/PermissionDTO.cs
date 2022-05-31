using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.DTOs
{
    public class PermissionDTO
    {
        public int IdPermission { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }
        public bool ForClient { get; set; }
    }
}
