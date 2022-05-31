using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.QueryDTOs
{
    public class UserResetDTO
    {
        public int IdUser { get; set; }
        public string? CurrentPassword { get; set; }
        public string? Password { get; set; }
    }
}
