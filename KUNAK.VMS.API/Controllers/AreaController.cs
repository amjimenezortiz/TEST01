using AutoMapper;
using KUNAK.VMS.CORE.DTOs;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.CORE.QueryFilters;
using KUNAK.VMS.INFRASTRUCTURE.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KUNAK.VMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;


        public AreaController(IAreaService areaService, IMapper mapper, IUriService uriService)
        {
            _areaService = areaService;
            _mapper = mapper;
            _uriService = uriService;

        }
        [HttpGet]
        public IActionResult GetAreas([FromQuery] AreaQueryFilter filters)
        {
            try
            {
                //var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));

                //if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:RA_User"].ToString()))
                //{
                var areas = _areaService.GetAreas(filters);
                var areasDtos = _mapper.Map<IEnumerable<AreaDTO>>(areas);
                return Ok(areasDtos);
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
        public async Task<IActionResult> GetArea(int id)
        {
            var areas = await _areaService.GetArea(id);
            var areasDto = _mapper.Map<AreaDTO>(areas);
            return Ok(areasDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostArea(AreaDTO areaDTO)
        {
            try
            {
                var area = _mapper.Map<Area>(areaDTO);
                await _areaService.InsertArea(area);
                var response = _mapper.Map<AreaDTO>(area);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, AreaDTO areaDTO)
        {
            try
            {
                var area = _mapper.Map<Area>(areaDTO);
                area.IdArea = id;
                await _areaService.UpdateArea(area);
                var areaEdit = await _areaService.GetArea(id);
                var response = _mapper.Map<AreaDTO>(areaEdit);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArea(int id)
        {
            try
            {
                var result = await _areaService.DeleteArea(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
