using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.DTOs
{
    public class ProviderDTO
    {
        public int IdProvider { get; set; }
        public int IdCompany { get; set; }
        public string? Ruc { get; set; }
        public string? CompanyName { get; set; }
        public string? EmployeeName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool? Status { get; set; }
    }
}
