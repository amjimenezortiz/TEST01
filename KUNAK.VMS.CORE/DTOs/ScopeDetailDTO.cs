using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.DTOs
{
    public class ScopeDetailDTO
    {
        public int IdScopeDetail { get; set; }
        public int IdScope { get; set; }
        public string? Name { get; set; }
        public string? ScopeDetailHtml { get; set; }
        public bool? Status { get; set; }

    }
}
