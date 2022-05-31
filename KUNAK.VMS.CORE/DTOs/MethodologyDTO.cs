using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.DTOs
{
    public class MethodologyDTO
    {
        public int IdMethodologie { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
    }
}
