using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.Entities
{
    public partial class User : BaseEntity
    {
        public User()
        {
            UsersHasPermissions = new HashSet<UsersHasPermission>();
        }

        public int IdUser { get; set; }
        public int IdRol { get; set; }
        public string IdenDoc { get; set; } = null!;
        public string? LastName { get; set; } 
        public string Name { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; } 
        public string Password { get; set; } = null!;
        public bool? Status { get; set; }

        public virtual Role IdRolNavigation { get; set; } = null!;
        public virtual ICollection<UsersHasPermission> UsersHasPermissions { get; set; }
    }
}
