using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
namespace KUNAK.VMS.CORE.DTOs
{
    public class CompanyDTO
    {
        public int IdCompany { get; set; }
        public string? Ruc { get; set; }
        public string? TradeName { get; set; }
        public string? Address { get; set; }
        public string? Region { get; set; }
        public string? Province { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool? Status { get; set; }
        public string? Logo { get; set; }
        public string? District { get; set; }
        public IFormFile? File { get; set; }
    }
}
