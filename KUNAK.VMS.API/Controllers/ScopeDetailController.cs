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
    public class ScopeDetailController : ControllerBase
    {
        private readonly IScopeDetailService _scopeDetailService;
        private readonly IMapper _mapper;


        public ScopeDetailController(IScopeDetailService scopeDetailService, IMapper mapper)
        {
            _scopeDetailService = scopeDetailService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetScopeDetails([FromQuery] ScopeDetailQueryFilter filters)
        {
            try
            {
                //var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));

                //if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:RA_User"].ToString()))
                //{
                var scopeDetails = _scopeDetailService.GetScopeDetails(filters);
                var scopeDetailsDtos = _mapper.Map<IEnumerable<ScopeDetailDTO>>(scopeDetails);
                return Ok(scopeDetailsDtos);
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
        public async Task<IActionResult> GetScopeDetail(int id)
        {
            var scopeDetails = await _scopeDetailService.GetScopeDetail(id);
            var scopeDetailsDto = _mapper.Map<ScopeDetailDTO>(scopeDetails);
            return Ok(scopeDetailsDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostScopeDetail(ScopeDetailDTO scopeDetailDTO)
        {
            try
            {
                var scopeDetail = _mapper.Map<ScopeDetail>(scopeDetailDTO);
                await _scopeDetailService.InsertScopeDetail(scopeDetail);
                var response = _mapper.Map<ScopeDetailDTO>(scopeDetail);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, ScopeDetailDTO scopeDetailDTO)
        {
            try
            {
                var scopeDetail = _mapper.Map<ScopeDetail>(scopeDetailDTO);
                scopeDetail.IdScopeDetail = id;
                await _scopeDetailService.UpdateScopeDetail(scopeDetail);
                var scopeDetailEdit = await _scopeDetailService.GetScopeDetail(id);
                var response = _mapper.Map<ScopeDetailDTO>(scopeDetailEdit);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScopeDetail(int id)
        {
            try
            {
                var result = await _scopeDetailService.DeleteScopeDetail(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("ScopeDetailIsRemoved/{idScopeDetail}")]
        public IActionResult ScopeDetailIsRemoved(int idScopeDetail)
        {
            try
            {
                var result = _scopeDetailService.ScopeDetailIsRemoved(idScopeDetail);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
