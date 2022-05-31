using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.QueryFilters
{
    public class UsersHasPermissionQueryFilter
    {
        public int IdUserPermission { get; set; }
        public string? Description { get; set; }
        public DateTime Stardate { get; set; }
        public DateTime Enddate { get; set; }
        public string? Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Status { get; set; }
        public int IdPermission { get; set; }
        public int IdUser { get; set; }
    }
}
