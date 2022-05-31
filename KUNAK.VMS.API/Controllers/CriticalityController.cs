using AutoMapper;
using KUNAK.VMS.API.Responses;
using KUNAK.VMS.CORE.CustomEntities;
using KUNAK.VMS.CORE.DTOs;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.CORE.QueryFilters;
using KUNAK.VMS.INFRASTRUCTURE.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KUNAK.VMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CriticalityController : ControllerBase
    {
        private readonly ICriticalityService _criticalityService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;


        public CriticalityController(ICriticalityService criticalityService, IMapper mapper, IUriService uriService)
        {
            _criticalityService = criticalityService;
            _mapper = mapper;
            _uriService = uriService;

        }
        [HttpGet]
        public IActionResult GetCriticalitys([FromQuery] CriticalityQueryFilter filters)
        {
            try
            {
                //var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));

                //if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:RA_User"].ToString()))
                //{
                var criticalities = _criticalityService.GetCriticalities(filters);
                var criticalitiesDtos = _mapper.Map<IEnumerable<CriticalityDTO>>(criticalities);
                return Ok(criticalitiesDtos);
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
        public async Task<IActionResult> GetCriticality(int id)
        {
            var criticalities = await _criticalityService.GetCriticality(id);
            var criticalitiesDto = _mapper.Map<CriticalityDTO>(criticalities);
            return Ok(criticalitiesDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostCriticality(CriticalityDTO criticalityDTO)
        {
            try
            {
                var criticality = _mapper.Map<Criticality>(criticalityDTO);
                await _criticalityService.InsertCriticality(criticality);
                var response = _mapper.Map<CriticalityDTO>(criticality);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, CriticalityDTO criticalityDTO)
        {
            try
            {
                var criticality = _mapper.Map<Criticality>(criticalityDTO);
                criticality.IdCriticality = id;
                await _criticalityService.UpdateCriticality(criticality);
                var criticalityEdit = await _criticalityService.GetCriticality(id);
                var response = _mapper.Map<CriticalityDTO>(criticalityEdit);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCriticality(int id)
        {
            try
            {
                var result = await _criticalityService.DeleteCriticality(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
