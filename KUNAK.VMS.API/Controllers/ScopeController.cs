using AutoMapper;
using KUNAK.VMS.API.Responses;
using KUNAK.VMS.CORE.CustomEntities;
using KUNAK.VMS.CORE.DTOs;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.CORE.QueryFilters;
using KUNAK.VMS.INFRASTRUCTURE.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KUNAK.VMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ScopeController : ControllerBase
    {
        private readonly IScopeService _scopeService;
        private readonly IMapper _mapper;


        public ScopeController(IScopeService scopeService, IMapper mapper)
        {
            _scopeService = scopeService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetScopes([FromQuery] ScopeQueryFilter filters)
        {
            try
            {
                //var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));

                //if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:RA_User"].ToString()))
                //{
                var scopes = _scopeService.GetScopes(filters);
                var scopesDtos = _mapper.Map<IEnumerable<ScopeDTO>>(scopes);
                return Ok(scopesDtos);
                //}
                //else
                //{
                //    return BadRequest("No tiene permiso para realizar esta operación");
                //}
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetScope(int id)
        {
            var scopes = await _scopeService.GetScope(id);
            var scopesDto = _mapper.Map<ScopeDTO>(scopes);
            return Ok(scopesDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostScope(ScopeDTO scopeDTO)
        {
            try
            {
                var scope = _mapper.Map<Scope>(scopeDTO);
                await _scopeService.InsertScope(scope);
                var response = _mapper.Map<ScopeDTO>(scope);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, ScopeDTO scopeDTO)
        {
            try
            {
                var scope = _mapper.Map<Scope>(scopeDTO);
                scope.IdScope = id;
                await _scopeService.UpdateScope(scope);
                var scopeEdit = await _scopeService.GetScope(id);
                var response = _mapper.Map<ScopeDTO>(scopeEdit);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScope(int id)
        {
            try
            {
                var result = await _scopeService.DeleteScope(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("ScopeIsRemoved/{idScope}")]
        public IActionResult ScopeIsRemoved(int idScope)
        {
            try
            {
                var result = _scopeService.ScopeIsRemoved(idScope);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet("GetScopesByIdVulnerabilityAssessment/{idVulnerabilityAssessment}")]
        public IActionResult GetScopesByIdVulnerabilityAssessment(int idVulnerabilityAssessment)
        {
            try
            {
                //var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));

                //if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:RA_User"].ToString()))
                //{
                var scopes = _scopeService.GetScopesByIdVulnerabilityAssessment(idVulnerabilityAssessment);
                var scopesDtos = _mapper.Map<IEnumerable<ScopeDTO>>(scopes);
                return Ok(scopesDtos);
                //}
                //else
                //{
                //    return BadRequest("No tiene permiso para realizar esta operación");
                //}
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
