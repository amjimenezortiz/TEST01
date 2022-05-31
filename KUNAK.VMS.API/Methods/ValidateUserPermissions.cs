using KUNAK.VMS.API.Interfaces;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KUNAK.VMS.API.Methods
{
    public class ValidateUserPermissions : IValidateUserPermissions
    {
        private readonly IUserService _userService;
        public ValidateUserPermissions(IUserService userService)
        {
            _userService = userService;
        }

        public bool Validation(JwtSecurityToken token, string permission)
        {
            int idRol = int.Parse(token.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value);
            string email= token.Claims.FirstOrDefault(x => x.Type == "Email").Value;
            User user = _userService.GetUserByEmail(email);
            return _userService.UserPermissionValidation(user.IdUser, idRol, permission);
        }

        public bool RolePermissionValidation(JwtSecurityToken token, string permission)
        {
            int idRol = int.Parse(token.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value);
            return _userService.RolePermissionValidation(idRol, permission);
        }

        public bool UserPermissionValidation(JwtSecurityToken token, string permission)
        {
            string email = token.Claims.FirstOrDefault(x => x.Type == "Email").Value;
            User user = _userService.GetUserByEmail(email);
            return _userService.UserPermissionValidation(user.IdUser, permission);
        }

    }
}
