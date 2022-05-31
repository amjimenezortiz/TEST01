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
    public class AssetTypeController : ControllerBase
    {
        private readonly IAssetTypeService _assetTypeService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;


        public AssetTypeController(IAssetTypeService assetTypeService, IMapper mapper, IUriService uriService)
        {
            _assetTypeService = assetTypeService;
            _mapper = mapper;
            _uriService = uriService;

        }
        [HttpGet]
        public IActionResult GetAssetTypes([FromQuery] AssetTypeQueryFilter filters)
        {
            try
            {
                //var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));

                //if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:RA_User"].ToString()))
                //{
                var assetTypes = _assetTypeService.GetAssetTypes(filters);
                var assetTypesDtos = _mapper.Map<IEnumerable<AssetTypeDTO>>(assetTypes);
                return Ok(assetTypesDtos);
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
        public async Task<IActionResult> GetAssetType(int id)
        {
            var assetTypes = await _assetTypeService.GetAssetType(id);
            var assetTypesDto = _mapper.Map<AssetTypeDTO>(assetTypes);
            return Ok(assetTypesDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostAssetType(AssetTypeDTO assetTypeDTO)
        {
            try
            {
                var assetType = _mapper.Map<AssetType>(assetTypeDTO);
                await _assetTypeService.InsertAssetType(assetType);
                var response = _mapper.Map<AssetTypeDTO>(assetType);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, AssetTypeDTO assetTypeDTO)
        {
            try
            {
                var assetType = _mapper.Map<AssetType>(assetTypeDTO);
                assetType.IdAssetType = id;
                await _assetTypeService.UpdateAssetType(assetType);
                var assetTypeEdit = await _assetTypeService.GetAssetType(id);
                var response = _mapper.Map<AssetTypeDTO>(assetTypeEdit);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssetType(int id)
        {
            try
            {
                var result = await _assetTypeService.DeleteAssetType(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
