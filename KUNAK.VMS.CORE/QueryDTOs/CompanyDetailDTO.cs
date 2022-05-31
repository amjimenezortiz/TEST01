using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.QueryDTOs
{
    public class CompanyDetailDTO
    {
        public int IdCompany { get; set; }
        public string? Ruc { get; set; }
        public string? TradeName { get; set; }
        public string? Address { get; set; }
        public bool Status { get; set; }
        public string? Region { get; set; }
        public string? District { get; set; }
        public string? Province { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Logo { get; set; }
        public IFormFile? File { get; set; }
        //public virtual ICollection<CompanyHasFrameworkDTO> CompanyHasFrameworkDtos { get; set; }
    }
}
