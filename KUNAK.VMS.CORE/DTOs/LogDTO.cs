using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.DTOs
{
    public class LogDTO
    {
        public int IdLog { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
        public string? Event { get; set; }
        public string? Table { get; set; }
    }
}
