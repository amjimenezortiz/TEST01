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
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;


        public AssetController(IAssetService assetService, IMapper mapper, IUriService uriService)
        {
            _assetService = assetService;
            _mapper = mapper;
            _uriService = uriService;

        }
        [HttpGet]
        public IActionResult GetAssets([FromQuery] AssetQueryFilter filters)
        {
            try
            {
                //var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));

                //if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:RA_User"].ToString()))
                //{
                    var assets = _assetService.GetAssets(filters);
                    var assetsDtos = _mapper.Map<IEnumerable<AssetDTO>>(assets);
                    return Ok(assetsDtos);
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
        public async Task<IActionResult> GetAsset(int id)
        {
            var assets = await _assetService.GetAsset(id);
            var assetsDto = _mapper.Map<AssetDTO>(assets);
            return Ok(assetsDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsset(AssetDTO assetDTO)
        {
            try
            {
                var asset = _mapper.Map<Asset>(assetDTO);
                await _assetService.InsertAsset(asset);
                var response = _mapper.Map<AssetDTO>(asset);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, AssetDTO assetDTO)
        {
            try
            {
                var asset = _mapper.Map<Asset>(assetDTO);
                asset.IdAsset= id;
                await _assetService.UpdateAsset(asset);
                var assetEdit = await _assetService.GetAsset(id);
                var response = _mapper.Map<AssetDTO>(assetEdit);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            try
            {
                var result = await _assetService.DeleteAsset(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
