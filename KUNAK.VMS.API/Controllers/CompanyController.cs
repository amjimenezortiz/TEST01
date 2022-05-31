using AutoMapper;
using KUNAK.VMS.API.Interfaces;
using KUNAK.VMS.CORE.DTOs;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Exceptions;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.CORE.QueryDTOs;
using KUNAK.VMS.CORE.QueryFilters;
using KUNAK.VMS.INFRASTRUCTURE.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
namespace KUNAK.VMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IConfiguration _configuration;
        private readonly IValidateUserPermissions _validationUserPermissions;
        private readonly IBlobManagement _blobManagement;
        public CompanyController(ICompanyService companyService/*, ICompanyHasFrameworkService companyHasFrameworkService*/, IMapper mapper, IUriService uriService, IConfiguration configuration, IValidateUserPermissions validateUserPermissions, IBlobManagement blobManagement)
        {
            _companyService = companyService;
            //_companyHasFrameworkService = companyHasFrameworkService;
            _mapper = mapper;
            _uriService = uriService;
            _configuration = configuration;
            _validationUserPermissions = validateUserPermissions;
            _blobManagement = blobManagement;
        }

        /////////////////////////////// CRUD BASIC OPERATION FOR THIS ENTITY ///////////////////////////////////////////

        [HttpGet(Name = nameof(GetCompanies))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetCompanies([FromQuery] CompanyQueryFilter filters)
        {

            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
                if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:R_Company"].ToString()))
                {
                    var companies = _companyService.GetCompanies(filters).ToList();
                    var companiesDto = _mapper.Map<IEnumerable<CompanyDTO>>(companies);
                    foreach (var companieDto in companiesDto)
                    {
                        companieDto.Logo = _blobManagement.GetLink(companieDto.Ruc, companieDto.Logo);
                    }
                    return Ok(companiesDto);
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

        [HttpGet("GetCompany")]
        public IActionResult GetCompany()
        {
            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
                if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:RO_Company"].ToString()))
                {
                    int idCompany = int.Parse(token.Claims.FirstOrDefault(x => x.Type == "Company").Value);
                    var companies = _companyService.GetCompany(idCompany);
                    var companiesDto = _mapper.Map<CompanyDTO>(companies);
                    return Ok(companiesDto);
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

        [HttpPost]
        public async Task<IActionResult> PostCompany([FromForm] CompanyDTO companyDTO)
        {
            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
                if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:C_Company"].ToString()))
                {
                    var company = _mapper.Map<Company>(companyDTO);
                    company.Logo = companyDTO.File.FileName;
                    await _companyService.InsertCompany(company);

                    var companyFrameworkDTO = _mapper.Map<CompanyDTO>(company);

                    //CREATE CONTAINER
                    await _blobManagement.CreateContainer(companyFrameworkDTO.Ruc);
                    await _blobManagement.ChangeAccessPolicyAsync(companyFrameworkDTO.Ruc);
                    await _blobManagement.UploadBlob(companyFrameworkDTO.Ruc, companyDTO.File);

                    companyFrameworkDTO.Logo = _blobManagement.GetLink(companyFrameworkDTO.Ruc, companyFrameworkDTO.Logo);

                    return Ok(companyFrameworkDTO);
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
        public async Task<IActionResult> PutCompany(int id, [FromForm] CompanyDTO companyFrameworkDTO)
        {
            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
                if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:U_Company"].ToString()))
                {
                    if (companyFrameworkDTO.File != null)
                    {
                        var lastLogo = _companyService.GetCompany(id).Logo;
                        //Guardamos en bd
                        //We save in database
                        Company company = _mapper.Map<Company>(companyFrameworkDTO);
                        company.IdCompany = id;
                        company.Logo = companyFrameworkDTO.File.FileName;
                        await _companyService.UpdateCompany(company);
                        //foreach (var item in companyFrameworkDTO.CompanyHasFrameworkDtos)
                        //{
                        //    CompanyHasFrameworkDTO companyHasFrameworkDTO = JsonConvert.DeserializeObject<CompanyHasFrameworkDTO>(item);
                        //    var companyHasFramework = _mapper.Map<CompanyHasFramework>(companyHasFrameworkDTO);
                        //    if (companyHasFramework.Status == false)
                        //    {
                        //        await _companyHasFrameworkService.DeleteCompanyHasFramework(
                        //            companyHasFramework.IdCompany, companyHasFramework.IdFramework);
                        //    }
                        //    else if (companyHasFramework.IdCompany == 0)
                        //    {
                        //        companyHasFramework.IdCompany = id;
                        //        await _companyHasFrameworkService.InsertCompanyHasFramework(companyHasFramework);
                        //    }
                        //}
                        var response = _mapper.Map<CompanyDTO>(_companyService.GetCompany(id));
                        //actualizamos el blob
                        //We update the blob
                        await _blobManagement.DeleteBlob(companyFrameworkDTO.Ruc, lastLogo);
                        await _blobManagement.UploadBlob(companyFrameworkDTO.Ruc, companyFrameworkDTO.File);
                        response.Logo = _blobManagement.GetLink(companyFrameworkDTO.Ruc, company.Logo);
                        return Ok(response);
                    }
                    else
                    {
                        var lastcompany = _companyService.GetCompany(id);
                        //Guardamos en bd
                        //We save in database
                        var company = _mapper.Map<Company>(companyFrameworkDTO);
                        company.IdCompany = id;
                        company.Logo = lastcompany.Logo;
                        await _companyService.UpdateCompany(company);
                        //foreach (var item in companyFrameworkDTO.CompanyHasFrameworkDtos)
                        //{
                        //    CompanyHasFrameworkDTO companyHasFrameworkDTO = JsonConvert.DeserializeObject<CompanyHasFrameworkDTO>(item);
                        //    var companyHasFramework = _mapper.Map<CompanyHasFramework>(companyHasFrameworkDTO);
                        //    if (companyHasFramework.Status == false)
                        //    {
                        //        await _companyHasFrameworkService.DeleteCompanyHasFramework(
                        //            companyHasFramework.IdCompany, companyHasFramework.IdFramework);
                        //    }
                        //    else if (companyHasFramework.IdCompany == 0)
                        //    {
                        //        companyHasFramework.IdCompany = id;
                        //        await _companyHasFrameworkService.InsertCompanyHasFramework(companyHasFramework);
                        //    }
                        //}
                        var response = _mapper.Map<CompanyDTO>(_companyService.GetCompany(id));
                        response.Logo = _blobManagement.GetLink(companyFrameworkDTO.Ruc, company.Logo);
                        return Ok(response);
                    }

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
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
                if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:D_Company"].ToString()))
                {
                    var result = await _companyService.DeleteCompany(id);
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
