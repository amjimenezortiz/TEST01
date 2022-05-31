using AutoMapper;
using KUNAK.VMS.API.Interfaces;
using KUNAK.VMS.CORE.DTOs;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.CORE.QueryDTOs;
using KUNAK.VMS.CORE.QueryFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KUNAK.VMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IRoleHasPermissionService _roleHasPermissionService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IValidateUserPermissions _validationUserPermissions;
        public RoleController(IRoleService roleService, IRoleHasPermissionService roleHasPermissionService, ICompanyService companyService, IMapper mapper, IConfiguration configuration, IValidateUserPermissions validateUserPermissions)
        {
            _roleService = roleService;
            _roleHasPermissionService = roleHasPermissionService;
            _companyService = companyService;
            _mapper = mapper;
            _configuration = configuration;
            _validationUserPermissions = validateUserPermissions;
        }
        /// <summary>
        /// Retorna todos los roles sin excepción
        /// Return all roles without exception
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>

        [HttpGet("GetAllRoles")]
        public IActionResult GetAllRoles([FromQuery] RoleQueryFilter filters)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
            var idRol = token.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:RA_Role"].ToString()))
            {
                //Obtener nombre de la compañía a la que pertenece el rol mediante un Join entre roles y compañías
                //Obtain the name of the company to which the role belongs through a Join between roles and companies
                var roles = _roleService.GetAllRoles(filters).ToList();
                var companies = _companyService.GetAllCompanies();
                var rolesDto = _mapper.Map<List<RoleDetailDTO>>(roles);
                var query = rolesDto.Join(companies,
                (role => role.IdCompany),
                (company => company.IdCompany),
                ((role, company) => new RoleDetailDTO
                {
                    IdRol = role.IdRol,
                    IdCompany = role.IdCompany,
                    Name = role.Name,
                    CompanyName = company.TradeName,
                    Status = role.Status,
                    PermissionsDtos = role.PermissionsDtos
                })).AsEnumerable();
                return Ok(query);
            }
            else if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:R_Role"].ToString())
                    || _validationUserPermissions.UserPermissionValidation(token, _configuration["Permissions:R_Role"].ToString()))
            {
                int idCompany = int.Parse(token.Claims.FirstOrDefault(x => x.Type == "Company").Value);
                var roles = _roleService.GetRolesByCompany(filters, idCompany);
                var rolesDto = _mapper.Map<IEnumerable<RoleDetailDTO>>(roles);
                return Ok(rolesDto);
            }
            else
            {
                return BadRequest("No tiene permiso para realizar esta operación");
            }
        }

        /// <summary>
        /// Retorna los roles dependiendo el idCompany que se recibe como parámetro
        /// Returns the roles depending on the idCompany that is received as a parameter
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>

        //[HttpGet("GetRolesByCompany")]
        //public IActionResult GetRolesByCompany([FromQuery] RoleQueryFilter filters)
        //{
        //    var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
        //    int idCompany = int.Parse(token.Claims.FirstOrDefault(x => x.Type == "Company").Value);
        //    var roles = _roleService.GetRolesByCompany(filters, idCompany);
        //    var rolesDto = _mapper.Map<IEnumerable<RoleDetailDTO>>(roles);
        //    return Ok(rolesDto);
        //}


        /////////////////////////////// CRUD BASIC OPERATION FOR THIS ENTITY ///////////////////////////////////////////


        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetRole(int id)
        //{
        //    var roles = await _roleService.GetRole(id);
        //    var rolesDto = _mapper.Map<RoleDetailDTO>(roles);
        //    return Ok(rolesDto);
        //}

        [HttpPost]
        public async Task<IActionResult> PostRole(RoleDTO roleDto)
        {
            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
                var validation = _validationUserPermissions.Validation(token, _configuration["Permissions:C_Role"].ToString());

                if (validation)
                {
                    int idCompany = int.Parse(token.Claims.FirstOrDefault(x => x.Type == "Company").Value);
                    var role = _mapper.Map<Role>(roleDto);
                    role.IdCompany = idCompany;
                    await _roleService.InsertRole(role);
                    var response = _mapper.Map<RoleDetailDTO>(role);
                    return Ok(response);
                }
                else
                {
                    return BadRequest("No tiene permiso para realizar esta operación");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, RolePermissionsDTO rolePermissionsDTO)
        {

            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
                var validation = _validationUserPermissions.Validation(token, _configuration["Permissions:U_Role"].ToString());

                if (validation)
                {
                    int idCompany = int.Parse(token.Claims.FirstOrDefault(x => x.Type == "Company").Value);
                    var roleDto = _mapper.Map<RoleDTO>(rolePermissionsDTO);
                    var role = _mapper.Map<Role>(roleDto);
                    role.IdRol = id;
                    role.IdCompany = idCompany;
                    await _roleService.UpdateRole(role);

                    //ir guardando cada uno de los permisos
                    //save each of the permissions
                    foreach (RoleHasPermissionDTO roleHasPermissionDto in rolePermissionsDTO.PermissionsDtos)
                    {
                        var roleHasPermission = _mapper.Map<RoleHasPermission>(roleHasPermissionDto);
                        if (roleHasPermission.IdRol == 0)
                        {
                            //insertar permiso
                            //insert permission
                            roleHasPermission.IdRol = id;
                            await _roleHasPermissionService.InsertRoleHasPermission(roleHasPermission);
                        }
                        else if (roleHasPermission.Status == false)
                        {
                            //eliminar permiso
                            //delete permission
                            await _roleHasPermissionService.DeleteRoleHasPermission(
                                roleHasPermission.IdRol, roleHasPermission.IdPermission);
                        }
                    }
                    //----------------------

                    role = await _roleService.GetRole(id);
                    rolePermissionsDTO = _mapper.Map<RolePermissionsDTO>(role);
                    return Ok(rolePermissionsDTO);
                }
                else
                {
                    return BadRequest("No tiene permiso para realizar esta operación");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
                var validation = _validationUserPermissions.Validation(token, _configuration["Permissions:D_Role"].ToString());

                if (validation)
                {
                    var result = await _roleService.DeleteRole(id);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("No tiene permiso para realizar esta operación");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
