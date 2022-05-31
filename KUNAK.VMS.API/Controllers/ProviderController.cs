using AutoMapper;
using KUNAK.VMS.API.Interfaces;
using KUNAK.VMS.API.Methods;
using KUNAK.VMS.CORE.DTOs;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Exceptions;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.CORE.QueryDTOs;
using KUNAK.VMS.CORE.QueryFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
namespace KUNAK.VMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IValidateUserPermissions _validationUserPermissions;

        public ProviderController(IProviderService providerService, IMapper mapper, IConfiguration configuration, IValidateUserPermissions validateUserPermissions)
        {
            _providerService = providerService;
            _mapper = mapper;
            _configuration = configuration;
            _validationUserPermissions = validateUserPermissions;
        }

        /// <summary>
        /// Retorna todos los proveedores sin excepción
        /// Return all providers without exception
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>

        [HttpGet("GetAllProviders")]
        public IActionResult GetAllProviders([FromQuery] ProviderQueryFilter filters)
        {
            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));

                if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:RA_Provider"].ToString()))
                {
                    var providers = _providerService.GetProviders(filters).ToList();
                    var providerCompanyDTO = _mapper.Map<IEnumerable<ProviderCompanyDTO>>(providers);
                    return Ok(providerCompanyDTO);
                }
                else if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:R_Provider"].ToString())
                    || _validationUserPermissions.UserPermissionValidation(token, _configuration["Permissions:R_Provider"].ToString()))
                {
                    int idCompany = int.Parse(token.Claims.FirstOrDefault(x => x.Type == "Company").Value);
                    var providers = _providerService.GetProvidersByCompany(filters, idCompany).ToList();
                    var providersDto = _mapper.Map<IEnumerable<ProviderDTO>>(providers);
                    return Ok(providersDto);
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

        /// <summary>
        /// Retorna los proveedores dependiendo el idCompany que se recibe como parámetro
        /// Returns the providers depending on the idCompany that is received as a parameter
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>


        //[HttpGet("GetProvidersByCompany")]
        //public IActionResult GetProvidersByCompany([FromQuery] ProviderQueryFilter filters)
        //{
        //    try
        //    {
        //        var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
        //        var validation = _validationUserPermissions.Validation(token, _configuration["Permissions:R_Provider"].ToString());

        //        if (validation)
        //        {
        //            int idCompany = int.Parse(token.Claims.FirstOrDefault(x => x.Type == "Company").Value);
        //            var providers = _providerService.GetProvidersByCompany(filters, idCompany);
        //            var providersDto = _mapper.Map<IEnumerable<ProviderDTO>>(providers);
        //            return Ok(providersDto);
        //        }
        //        else
        //        {
        //            return BadRequest("No tiene permiso para realizar esta operación");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e);
        //    }
        //}


        /////////////////////////////// CRUD BASIC OPERATION FOR THIS ENTITY ///////////////////////////////////////////

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetProvider(int id)
        //{
        //    try
        //    {
        //        var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
        //        var validation = _validationUserPermissions.Validation(token, _configuration["Permissions:R_Provider"].ToString());

        //        if (validation)
        //        {
        //            var providers = await _providerService.GetProvider(id);
        //            var providersDto = _mapper.Map<ProviderDTO>(providers);
        //            return Ok(providersDto);
        //        }
        //        else
        //        {
        //            return BadRequest("No tiene permiso para realizar esta operación");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e);
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> PostProvider(ProviderDTO providerDTO)
        {
            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
                var validation = _validationUserPermissions.Validation(token, _configuration["Permissions:C_Provider"].ToString());

                if (validation)
                {
                    int idCompany = int.Parse(token.Claims.FirstOrDefault(x => x.Type == "Company").Value);
                    var provider = _mapper.Map<Provider>(providerDTO);
                    provider.IdCompany = idCompany;
                    await _providerService.InsertProvider(provider);
                    providerDTO = _mapper.Map<ProviderDTO>(provider);
                    return Ok(providerDTO);
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
        public async Task<IActionResult> PutProvider(int id, ProviderDTO providerDTO)
        {
            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
                var validation = _validationUserPermissions.Validation(token, _configuration["Permissions:U_Provider"].ToString());
                if (validation)
                {
                    int idCompany = int.Parse(token.Claims.FirstOrDefault(x => x.Type == "Company").Value);
                    var provider = _mapper.Map<Provider>(providerDTO);
                    provider.IdProvider = id;
                    provider.IdCompany = idCompany;
                    await _providerService.UpdateProvider(provider);
                    providerDTO = _mapper.Map<ProviderDTO>(provider);
                    return Ok(providerDTO);
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
        public async Task<IActionResult> DeleteProvider(int id)
        {
            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
                var validation = _validationUserPermissions.Validation(token, _configuration["Permissions:D_Provider"].ToString());

                if (validation)
                {
                    int idCompany = int.Parse(token.Claims.FirstOrDefault(x => x.Type == "Company").Value);
                    var result = await _providerService.DeleteProvider(id, idCompany);
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
