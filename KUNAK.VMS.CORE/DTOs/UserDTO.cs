using System;
using System.Collections.Generic;

namespace KUNAK.VMS.CORE.DTOs
{
    public class UserDTO
    {
        public int IdUser { get; set; }
        public int IdRol { get; set; }
        public string? IdenDoc { get; set; }
        public string? LastName { get; set; }
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public bool? Status { get; set; }
    }
}
