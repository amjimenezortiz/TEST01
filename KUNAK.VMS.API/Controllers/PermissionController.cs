using AutoMapper;
using KUNAK.VMS.API.Interfaces;
using KUNAK.VMS.CORE.DTOs;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.CORE.QueryFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace KUNAK.VMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IValidateUserPermissions _validationUserPermissions;
        public PermissionController(IPermissionService permissionService, IMapper mapper, IConfiguration configuration, IValidateUserPermissions validateUserPermissions)
        {
            _permissionService = permissionService;
            _mapper = mapper;
            _configuration = configuration;
            _validationUserPermissions = validateUserPermissions;
        }

        [HttpGet("GetPermissionsAditional/{idUser}")]
        public IActionResult GetPermissionsAditional(int idUser)
        {
            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));

                if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:R_Permission"].ToString())
                        || _validationUserPermissions.UserPermissionValidation(token, _configuration["Permissions:R_Permission"].ToString()))
                {
                    var permissions = _permissionService.GetPermissionsAditional(idUser);
                    var permissionsDto = _mapper.Map<IEnumerable<PermissionDTO>>(permissions);
                    return Ok(permissionsDto);
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

        /////////////////////////////// CRUD BASIC OPERATION FOR THIS ENTITY ///////////////////////////////////////////

        /// <summary>
        /// Retorna todos los permisos sin excepción
        /// Return all permissions without exception
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>

        [HttpGet]
        public IActionResult GetAllPermissions([FromQuery] PermissionQueryFilter filters)
        {
            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));

                if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:RA_Permission"].ToString()))
                {
                    var permissions = _permissionService.GetPermissions(filters).ToList();
                    var permissionsDto = _mapper.Map<IEnumerable<PermissionDTO>>(permissions);
                    return Ok(permissionsDto);
                }
                else if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:R_Permission"].ToString())
                        || _validationUserPermissions.UserPermissionValidation(token, _configuration["Permissions:R_Permission"].ToString()))
                {
                    var permissions = _permissionService.GetPermissionsForClient(filters);
                    var permissionsDto = _mapper.Map<IEnumerable<PermissionDTO>>(permissions);
                    return Ok(permissionsDto);
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


        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetPermission(int id)
        //{
        //    var permissions = await _permissionService.GetPermission(id);
        //    var permissionsDto = _mapper.Map<PermissionDTO>(permissions);

        //    return Ok(permissionsDto);
        //}

        [HttpPost]
        public async Task<IActionResult> PostPermission(PermissionDTO permissionDTO)
        {
            var permission = _mapper.Map<Permission>(permissionDTO);
            await _permissionService.InsertPermission(permission);

            permissionDTO = _mapper.Map<PermissionDTO>(permission);
            return Ok(permissionDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermission(int id, PermissionDTO permissionDTO)
        {
            var permission = _mapper.Map<Permission>(permissionDTO);
            permission.IdPermission = id;
            await _permissionService.UpdatePermission(permission);
            permissionDTO = _mapper.Map<PermissionDTO>(permission);
            return Ok(permissionDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            var result = await _permissionService.DeletePermission(id);
            return Ok(result);
        }
    }
}
