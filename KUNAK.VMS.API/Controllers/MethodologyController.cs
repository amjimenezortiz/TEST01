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
    public class MethodologyController : ControllerBase
    {
        private readonly IMethodologyService _methodologyService;
        private readonly IMapper _mapper;


        public MethodologyController(IMethodologyService methodologyService, IMapper mapper)
        {
            _methodologyService = methodologyService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetMethodologys([FromQuery] MethodologyQueryFilter filters)
        {
            try
            {
                //var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));

                //if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:RA_User"].ToString()))
                //{
                var methodologys = _methodologyService.GetMethodologys(filters);
                var methodologysDtos = _mapper.Map<IEnumerable<MethodologyDTO>>(methodologys);
                return Ok(methodologysDtos);
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
        public async Task<IActionResult> GetMethodology(int id)
        {
            var methodologys = await _methodologyService.GetMethodology(id);
            var methodologysDto = _mapper.Map<MethodologyDTO>(methodologys);
            return Ok(methodologysDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostMethodology(MethodologyDTO methodologyDTO)
        {
            try
            {
                var methodology = _mapper.Map<Methodology>(methodologyDTO);
                await _methodologyService.InsertMethodology(methodology);
                var response = _mapper.Map<MethodologyDTO>(methodology);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, MethodologyDTO methodologyDTO)
        {
            try
            {
                var methodology = _mapper.Map<Methodology>(methodologyDTO);
                methodology.IdMethodologie = id;
                await _methodologyService.UpdateMethodology(methodology);
                var methodologyEdit = await _methodologyService.GetMethodology(id);
                var response = _mapper.Map<MethodologyDTO>(methodologyEdit);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMethodology(int id)
        {
            try
            {
                var result = await _methodologyService.DeleteMethodology(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
