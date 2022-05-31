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
    public class BlacklistPasswordController : ControllerBase
    {
        private readonly IBlacklistPasswordService _blacklistPasswordService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;


        public BlacklistPasswordController(IBlacklistPasswordService blacklistPasswordService, IMapper mapper, IUriService uriService)
        {
            _blacklistPasswordService = blacklistPasswordService;
            _mapper = mapper;
            _uriService = uriService;

        }
        [HttpGet]
        public IActionResult GetBlacklistPasswords([FromQuery] BlacklistPasswordQueryFilter filters)
        {
            try
            {
                //var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));

                //if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:RA_User"].ToString()))
                //{
                var blacklistPasswords = _blacklistPasswordService.GetBlacklistPasswords(filters);
                var blacklistPasswordsDtos = _mapper.Map<IEnumerable<BlacklistPasswordDTO>>(blacklistPasswords);
                return Ok(blacklistPasswordsDtos);
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
        public async Task<IActionResult> GetBlacklistPassword(int id)
        {
            var blacklistPasswords = await _blacklistPasswordService.GetBlacklistPassword(id);
            var blacklistPasswordsDto = _mapper.Map<BlacklistPasswordDTO>(blacklistPasswords);
            return Ok(blacklistPasswordsDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostBlacklistPassword(BlacklistPasswordDTO blacklistPasswordDTO)
        {
            try
            {
                var blacklistPassword = _mapper.Map<BlacklistPassword>(blacklistPasswordDTO);
                await _blacklistPasswordService.InsertBlacklistPassword(blacklistPassword);
                var response = _mapper.Map<BlacklistPasswordDTO>(blacklistPassword);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, BlacklistPasswordDTO blacklistPasswordDTO)
        {
            try
            {
                var blacklistPassword = _mapper.Map<BlacklistPassword>(blacklistPasswordDTO);
                blacklistPassword.IdPassword = id;
                await _blacklistPasswordService.UpdateBlacklistPassword(blacklistPassword);
                var blacklistPasswordEdit = await _blacklistPasswordService.GetBlacklistPassword(id);
                var response = _mapper.Map<BlacklistPasswordDTO>(blacklistPasswordEdit);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlacklistPassword(int id)
        {
            try
            {
                var result = await _blacklistPasswordService.DeleteBlacklistPassword(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
