using System.IdentityModel.Tokens.Jwt;

namespace KUNAK.VMS.API.Interfaces
{
    public interface IValidateUserPermissions
    {
        bool Validation(JwtSecurityToken token, string permission);
        bool RolePermissionValidation(JwtSecurityToken token, string permission);
        bool UserPermissionValidation(JwtSecurityToken token, string permission);
    }
}
