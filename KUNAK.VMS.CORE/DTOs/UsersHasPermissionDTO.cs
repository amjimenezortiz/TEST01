using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.DTOs
{
    public class UsersHasPermissionDTO
    {
        public int IdUserPermission { get; set; }
        public int IdPermission { get; set; }
        public int IdUser { get; set; }
        public string? Description { get; set; }
        public DateTime Stardate { get; set; }
        public DateTime Enddate { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? Username { get; set; }
    }
}
