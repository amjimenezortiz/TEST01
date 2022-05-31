using AutoMapper;
using KUNAK.VMS.CORE.DTOs;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.CORE.QueryDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KUNAK.VMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IMapper mapper, IUserService userService)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Authentication(UserLogin login)
        {

            var validation = await IsValidUser(login);
            if (validation.Item1)
            {
                var token = GenerateToken(validation.Item2);
                var userdto = _mapper.Map<RoleLoginDTO>(validation.Item2);
                User user = await _userService.GetAditionalPermissions(validation.Item2.IdUser);
                var usersHasPermission = _mapper.Map<IEnumerable<PermissionDTO>>(user.UsersHasPermissions);
                userdto.UserPermissions = usersHasPermission.ToList();
                return Ok(new
                {
                    user = userdto,
                    accessToken = token,
                    tokenType = "bearer"
                });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("access-token")]
        public IActionResult RenewToken(UserToken userToken)
        {
            if (ValidateToken(userToken))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.ReadToken(userToken.Token.ToString()) as JwtSecurityToken;
                var claims = securityToken.Claims.ToList();
                var emailClaim = claims[1].Value;
                var user = _userService.GetUserByEmail(emailClaim);
                var userdto = _mapper.Map<RoleLoginDTO>(user);
                var newToken = GenerateToken(user);
                return Ok(new
                {
                    user = userdto,
                    accessToken = newToken,
                    tokenType = "bearer"
                });
            }
            else
            {
                return BadRequest("Token expirado, inicie sesión nuevamente");
            }
        }

        [HttpGet("test-connection")]
        public IActionResult Testconnection()
        {
            return Ok("Al menos conecta");
        }

        private async Task<(bool, User)> IsValidUser(UserLogin User)
        {
            var user = await _userService.GetLogiByCredentials(User);
            return (user != null, user);
        }

        private string GenerateToken(User User)
        {
            //Header
            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, string.Concat(User.Name, " ", User.LastName)),
                new Claim(ClaimTypes.Email, User.Email),
                new Claim("Email", User.Email),
                new Claim(ClaimTypes.Role, User.IdRol.ToString()),
                new Claim("Company", User.IdRolNavigation.IdCompany.ToString()),
                ///new 
                new Claim("idUser", User.IdUser.ToString()),
                new Claim("idRole", User.IdRol.ToString()),
            };

            //Payloads
            var payload = new JwtPayload
            (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(15)
            );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool ValidateToken(UserToken userToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters();

                tokenHandler.ValidateToken(userToken.Token.ToString(), validationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = _configuration["Authentication:Issuer"],
                ValidAudience = _configuration["Authentication:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]))
            };
        }
        private string GenerateToken()
        {
            //Header
            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Antonio Jimenez"),
                new Claim(ClaimTypes.Email, "amjimenezortiz@gmail.com"),
                new Claim(ClaimTypes.Role, "AdminKunak")
            };

            //Payload
            var payload = new JwtPayload(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(60)
            );

            //Signature
            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
