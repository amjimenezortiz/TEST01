using AutoMapper;
using Azure;
using Azure.Storage.Blobs.Models;
using KUNAK.VMS.API.Interfaces;
using KUNAK.VMS.CORE.DTOs;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.CORE.QueryDTOs;
using KUNAK.VMS.CORE.QueryFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
namespace KUNAK.VMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EvidenceController : ControllerBase
    {
        private readonly IEvidenceService _evidenceService;
        private readonly IVulnerabilityAssessmentService _vulnerabilityAssessmentService;
        private readonly IUserService _userService;
        private readonly IBlobManagement _BlobManagement;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IValidateUserPermissions _validationUserPermissions;
        public EvidenceController(IEvidenceService evidenceService, ICompanyService companyService, IVulnerabilityAssessmentService vulnerabilityAssessmentService, IUserService userService, IBlobManagement BlobManagement, IMapper mapper, IConfiguration configuration, IValidateUserPermissions validateUserPermissions)
        {
            _evidenceService = evidenceService;
            _companyService = companyService;
            _vulnerabilityAssessmentService = vulnerabilityAssessmentService;
            _userService = userService;
            _BlobManagement = BlobManagement;
            _mapper = mapper;
            _configuration = configuration;
            _validationUserPermissions = validateUserPermissions;
        }

        [HttpGet("{idVulnerabilityAssessment}")]
        public IActionResult GetEvidences(int idVulnerabilityAssessment)
        {
            try
            {
                //Actualizar permisos
                //var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
                //if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:R_Company"].ToString()))
                //{
                var evidences = _evidenceService.GetEvidences(idVulnerabilityAssessment);
                var evidencesDTO = _mapper.Map<IEnumerable<EvidenceDTO>>(evidences);
                return Ok(evidencesDTO);

                //}
                //else
                //{
                //    return BadRequest("No tiene permiso para realizar esta operación");
                //}
            }
            catch (Exception e)
            {
                return BadRequest(e);
                //throw new BusinessException("Token no ingresado");

            }
        }
        //Crud
        [HttpGet("GetEvidencesDetail")]
        public async Task<IActionResult> GetEvidencesDetail([FromQuery] EvidenceQueryFilter filters)
        {
            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
                if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:R_Evidence"].ToString())
                    || _validationUserPermissions.UserPermissionValidation(token, _configuration["Permissions:R_Evidence"].ToString()))
                {
                    int idCompany = int.Parse(token.Claims.FirstOrDefault(x => x.Type == "Company").Value);
                    var idUser = int.Parse(token.Claims.FirstOrDefault(x => x.Type == "idUser").Value);
                    var company = _companyService.GetCompany(idCompany);
                    List<dynamic> responses = new();
                    List<dynamic> evidencesResponse = new();
                    List<dynamic> folders = new();
                    List<dynamic> files = new();
                    List<dynamic> path = new();
                    //Obtener avidencias que pertenecen al idGap ingresado
                    //Obtain evidence that belongs to the entered idGap
                    if (filters.FolderId > 0)
                    {
                        //var evidences = _evidenceService.GetEvidencesDetail(filters.FolderId);
                        var evidences = _evidenceService.GetEvidencesDetail(filters.FolderId);
                        foreach (var evidence in evidences)
                        {
                            var evidenceResponse = _mapper.Map<VulnerabilityAssessmentEvidenceDTO>(evidence);
                            evidenceResponse.File = "/Evidence/DownloadEvidence/" + evidenceResponse.Id;
                            evidencesResponse.Add(evidenceResponse);
                        }
                        VulnerabilityAssessment vulnerabilityAssessment = await _vulnerabilityAssessmentService
                            .GetVulnerabilityAssessment(filters.FolderId);
                        //Se Ha puesto el idUser del token ya que la revisión no tiene
                        User user = await _userService.GetUser(idUser);
                        var response = _mapper.Map<VulnerabilityAssessmentEvidenceDTO>(vulnerabilityAssessment);
                        response.CreatedBy = user.LastName + " " + user.Name;
                        response.Type = "Folder";
                        response.Description = "Evidencias utilizadas en la revisión " + vulnerabilityAssessment.Name;
                        response.Contents = evidences.Count() + " files";
                        path.Add(response);
                    }
                    //Obtener los gap que pertenecen a la compañía ingresada
                    //Obtain the gaps that belong to the entered company
                    else
                    {
                        var vulnerabilityAssessments = _vulnerabilityAssessmentService.GetVulnerabilityAssessmentsByIdCompany(idCompany).ToList();

                        foreach (var vulnerabilityAssessment in vulnerabilityAssessments)
                        {
                            //Se Ha puesto el idUser del token ya que la revisión no tiene
                            User user = await _userService.GetUser(idUser);
                            
                            var response = _mapper.Map<VulnerabilityAssessmentEvidenceDTO>(vulnerabilityAssessments);
                            response.CreatedBy = user.LastName + " " + user.Name;
                            response.Type = "Folder";
                            response.Description = "Evidencias utilizadas en el analisis " + vulnerabilityAssessment.Name;
                            var evidences = _evidenceService.GetEvidencesDetail(vulnerabilityAssessment.IdVulnerabilityAssessment);
                            response.Contents = evidences.Count() + " files";
                            folders.Add(response);
                        }
                    }
                    var item = new
                    {
                        files = evidencesResponse,
                        folders,
                        path
                    };
                    return Ok(item);
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
        public async Task<IActionResult> DeleteEvidence(int id)
        {
            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
                if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:D_Evidence"].ToString())
                    || _validationUserPermissions.UserPermissionValidation(token, _configuration["Permissions:D_Evidence"].ToString()))
                {
                    //Eliminamos el blob
                    //We delete the blob
                    int idCompany = int.Parse(token.Claims.FirstOrDefault(x => x.Type == "Company").Value);
                    var company = _companyService.GetCompany(idCompany);
                    Evidence evidence = await _evidenceService.GetEvidence(id);
                    var containerName = company.Ruc + "-revisión-" + evidence.IdVulnerabilityAssessment;
                    //-- eliminamos el archivo
                    //-- we delete the file
                    await _BlobManagement.DeleteBlob(containerName, evidence.Filename);

                    //Eliminamos todo referente a la evidencia
                    //We eliminate everything related to the evidence
                    var result = await _evidenceService.DeleteEvidence(id);
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

        [HttpGet("DownloadEvidence/{idEvidence}")]
        public async Task<FileResult> DownloadEvidence(int idEvidence)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
            if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:DW_Evidence"].ToString())
                || _validationUserPermissions.UserPermissionValidation(token, _configuration["Permissions:DW_Evidence"].ToString()))
            {
                int idCompany = int.Parse(token.Claims.FirstOrDefault(x => x.Type == "Company").Value);
                var company = _companyService.GetCompany(idCompany);
                Evidence evidence = await _evidenceService.GetEvidence(idEvidence);
                var containerName = company.Ruc + "-revisión-" + evidence.IdVulnerabilityAssessment;
                BlobDownloadInfo file = await _BlobManagement.DownloadBlob(containerName, evidence.Filename);
                return File(file.Content, file.ContentType, evidence.Filename);
            }
            else
            {
                return null;
            }

        }

    }
}
