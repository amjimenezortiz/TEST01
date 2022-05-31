using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.QueryDTOs
{
    public class ProviderCompanyDTO
    {
        public int IdProvider { get; set; }
        public int IdCompany { get; set; }
        public string ?EmployeeName { get; set; }
        public string ?Phone { get; set; }
        public string ?Email { get; set; }
        public bool ?Status { get; set; }
        public string ?CompanyName { get; set; }
        public string ?Ruc { get; set; }
        public string ?TradeName { get; set; }
    }
}
