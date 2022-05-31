using AutoMapper;
using KUNAK.VMS.CORE.DTOs;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.QueryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.INFRASTRUCTURE.Mappings
{
    class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<AditionalDetail, AditionalDetailDTO>().ReverseMap();

            CreateMap<Area, AreaDTO>().ReverseMap();

            CreateMap<Asset, AssetDTO>().ReverseMap();

            CreateMap<AssetType, AssetTypeDTO>().ReverseMap();

            CreateMap<BlacklistPassword, BlacklistPasswordDTO>().ReverseMap();

            CreateMap<Company, CompanyDTO>().ReverseMap();

            CreateMap<Criticality, CriticalityDTO>().ReverseMap();

            CreateMap<ConsultantHasVulnerabilityAssessment, ConsultantHasVulnerabilityAssessmentDTO>().ReverseMap();

            CreateMap<DetailHasEvidence, DetailHasEvidenceDTO>().ReverseMap();
            
            CreateMap<Evidence, EvidenceDTO>().ReverseMap();

            CreateMap<Evidence, VulnerabilityAssessmentEvidenceDTO>()
                .ForMember(gapEvidenceDTO => gapEvidenceDTO.Id, prop => prop.MapFrom(evidence => evidence.IdEvidence))
                .ForMember(gapEvidenceDTO => gapEvidenceDTO.FolderId, prop => prop.MapFrom(evidence => evidence.IdVulnerabilityAssessment))
                .ForMember(gapEvidenceDTO => gapEvidenceDTO.Name, prop => prop.MapFrom(evidence => evidence.Filename))
                .ForMember(gapEvidenceDTO => gapEvidenceDTO.CreatedAt, prop => prop.MapFrom(evidence => evidence.Date))
                .ForMember(gapEvidenceDTO => gapEvidenceDTO.Type, prop => prop.MapFrom(evidence => evidence.FileExtension))
                //.ForMember(gapEvidenceDTO => gapEvidenceDTO.FrameworkThirdlevels,
                //prop => prop.MapFrom(Evidence => Evidence.ResultsHasEvidences.Select(x => x.Id.IdThirdLevelNavigation).ToList()))
                .ReverseMap();

            CreateMap<Log, LogDTO>().ReverseMap();

            CreateMap<Methodology, MethodologyDTO>().ReverseMap();

            CreateMap<Permission, PermissionDTO>().ReverseMap();
            
            CreateMap<Person, PersonDTO>().ReverseMap();

            CreateMap<Provider, ProviderDTO>().ReverseMap();

            CreateMap<Role, RoleDTO>().ReverseMap();

            CreateMap<RoleHasPermission, RoleHasPermissionDTO>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();


            CreateMap<UsersHasPermission, UsersHasPermissionDTO>().ReverseMap();

            CreateMap<VulAssementDetailHasAsset, VulAssementDetailHasAssetDTO>().ReverseMap();

            CreateMap<VulnerabilityAssementDetailStatus, VulnerabilityAssementDetailStatusDTO>().ReverseMap();

            CreateMap<VulnerabilityAssessment, VulnerabilityAssessmentDTO>().ReverseMap();

            CreateMap<VulnerabilityAssessmentDetail, VulnerabilityAssessmentDetailDTO>()
                .ForMember(VulnerabilityAssessmentDetailDTO => VulnerabilityAssessmentDetailDTO.AditionalDetailsDtos, 
                prop => prop.MapFrom(VulnerabilityAssessmentDetail => VulnerabilityAssessmentDetail.AditionalDetails))
                .ReverseMap();



            CreateMap<VulnerabilityAssessmentDetail, VulnerabilityAssessmentDetailPostDTO>().ReverseMap();

            CreateMap<VulnerabilityAssessmentHasMethodology, VulnerabilityAssessmentHasMethodologyDTO>().ReverseMap();

            CreateMap<VulnerabilityState, VulnerabilityStateDTO>().ReverseMap();
            //CreateMap<Evidence, EvidenceResultsDTO>()
            //    .ForMember(EvidenceResultsDTO => EvidenceResultsDTO.FrameworkThirdlevels,
            //    prop => prop.MapFrom(Evidence => Evidence.ResultsHasEvidences.Select(x => x.Id.IdThirdLevelNavigation).ToList()))
            //    .ReverseMap();

            //CreateMap<Evidence, GapEvidenceDTO>()
            //    .ForMember(gapEvidenceDTO => gapEvidenceDTO.Id, prop => prop.MapFrom(evidence => evidence.IdEvidence))
            //    .ForMember(gapEvidenceDTO => gapEvidenceDTO.FolderId, prop => prop.MapFrom(evidence => evidence.IdGapAnalysis))
            //    .ForMember(gapEvidenceDTO => gapEvidenceDTO.Name, prop => prop.MapFrom(evidence => evidence.Filename))
            //    .ForMember(gapEvidenceDTO => gapEvidenceDTO.CreatedAt, prop => prop.MapFrom(evidence => evidence.Date))
            //    .ForMember(gapEvidenceDTO => gapEvidenceDTO.Type, prop => prop.MapFrom(evidence => evidence.FileExtension))
            //    .ForMember(gapEvidenceDTO => gapEvidenceDTO.FrameworkThirdlevels,
            //    prop => prop.MapFrom(Evidence => Evidence.ResultsHasEvidences.Select(x => x.Id.IdThirdLevelNavigation).ToList()))
            //    .ReverseMap();

            //CreateMap<Framework, FrameworkDTO>().ReverseMap();

            //CreateMap<FrameworkFirstlevel, FrameworkFirstlevelDTO>().ReverseMap();

            //CreateMap<FrameworkSecondlevel, FrameworkSecondlevelDTO>().ReverseMap();

            //CreateMap<FrameworkThirdlevel, FrameworkThirdlevelDTO>().ReverseMap();

            //CreateMap<GapAnalyze, GapAnalyzeDTO>().ReverseMap();
            //CreateMap<GapAnalyze, GapAnalyzeDetailDTO>()
            //    .ForMember(gapAnalyzeDetailDTO => gapAnalyzeDetailDTO.FrameworkFirstlevels, prop => prop.MapFrom(gapAnalyze => gapAnalyze.IdFrameworkNavigation.FrameworkFirstlevels))
            //    .ReverseMap();

            //CreateMap<GapAnalyze, GapEvidenceDTO>()
            //    .ForMember(gapEvidenceDTO => gapEvidenceDTO.Id, prop => prop.MapFrom(gapAnalyze => gapAnalyze.IdGapAnalysis))
            //    .ForMember(gapEvidenceDTO => gapEvidenceDTO.Name, prop => prop.MapFrom(gapAnalyze => gapAnalyze.Name))
            //    //.ForMember(gapEvidenceDTO => gapEvidenceDTO.CreatedBy, prop => prop.MapFrom(gapAnalyze => gapAnalyze.Responsable))
            //    .ForMember(gapEvidenceDTO => gapEvidenceDTO.CreatedAt, prop => prop.MapFrom(gapAnalyze => gapAnalyze.CreatedAt))
            //    .ForMember(gapEvidenceDTO => gapEvidenceDTO.ModifiedAt, prop => prop.MapFrom(gapAnalyze => gapAnalyze.UpdatedAt))
            //    .ReverseMap();

            //CreateMap<GapControlResult, GapControlResultDTO>().ReverseMap();

            //CreateMap<HeatmapDetail, HeatmapDetailDTO>().ReverseMap();

            //CreateMap<ImpactLevel, ImpactLevelDTO>().ReverseMap();

            //CreateMap<MadurityLevel, MadurityLevelDTO>().ReverseMap();

            //CreateMap<MadurityModel, MadurityModelDTO>().ReverseMap();

            //CreateMap<Permission, PermissionDTO>().ReverseMap();

            //CreateMap<ProbabilityLevel, ProbabilityLevelDTO>().ReverseMap();

            //CreateMap<Project, ProjectDTO>().ReverseMap();

            //CreateMap<Project, ProjectResultsDTO>()
            //    .ForMember(projectResultsDTO => projectResultsDTO.GapAnalyzeName,
            //    prop => prop.MapFrom(project => project.ProjectHasGapResults.Select(x => x.Id.IdGapAnalysisNavigation.Name).FirstOrDefault()))
            //    .ForMember(projectResultsDTO => projectResultsDTO.ProjectHasGapResultDtos,
            //    prop => prop.MapFrom(project => project.ProjectHasGapResults))
            //    .ReverseMap();

            //CreateMap<ProjectHasGapResult, ProjectHasGapResultDTO>().ReverseMap();

            //CreateMap<ProjectHasRisk, ProjectHasRiskDTO>().ReverseMap();

            //CreateMap<ProjectImage, ProjectImageDTO>().ReverseMap();

            //CreateMap<Provider, ProviderDTO>().ReverseMap();
            CreateMap<Provider, ProviderCompanyDTO>()
                //.ForMember(providercompany => providercompany.TradeName, prop => prop.MapFrom(provider => provider.IdCompanyNavigation.TradeName))
                .ReverseMap();

            CreateMap<Scope, ScopeDTO>()
                .ForMember(ScopeDTO => ScopeDTO.ScopeDetails, prop => prop.MapFrom(Scope => Scope.ScopeDetails))
                .ReverseMap();

            CreateMap<ScopeDetail, ScopeDetailDTO>().ReverseMap();

            //CreateMap<Risk, RiskDTO>().ReverseMap();

            //CreateMap<RiskModel, RiskModelDTO>().ReverseMap();

            //CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<Role, RoleDetailDTO>()
                .ForMember(roleDetail => roleDetail.PermissionsDtos, prop => prop.MapFrom(Role => Role.RoleHasPermissions.Select(x => x.IdPermissionNavigation).ToList()))
                .ReverseMap();
            CreateMap<Role, RolePermissionsDTO>()
                .ForMember(rolePermission => rolePermission.PermissionsDtos, prop => prop.MapFrom(Role => Role.RoleHasPermissions))
                .ReverseMap();

            CreateMap<RoleDTO, RolePermissionsDTO>().ReverseMap();

            CreateMap<RoleHasPermission, RoleHasPermissionDTO>().ReverseMap();

            //CreateMap<Threat, ThreatDTO>().ReverseMap();

            //CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, RoleLoginDTO>()
                .ForMember(userrol => userrol.IdCompany, prop => prop.MapFrom(user => user.IdRolNavigation.IdCompany))
                .ForMember(userrol => userrol.RoleName, prop => prop.MapFrom(user => user.IdRolNavigation.Name))
                .ForMember(rolePermissions => rolePermissions.RolePermissions, prop => prop.MapFrom(user => user.IdRolNavigation.RoleHasPermissions.Select(x => x.IdPermissionNavigation).ToList()))
                //.ForMember(userPermissions => userPermissions.UserPermissions, prop => prop.MapFrom(user => user.UsersHasPermissions.Select(x => x.IdPermissionNavigation).ToList()))
                .ReverseMap();
            CreateMap<User, UserDetailDTO>()
                .ForMember(usercompany => usercompany.TradeName, prop => prop.MapFrom(user => user.IdRolNavigation.IdCompanyNavigation.TradeName))
                .ForMember(userrol => userrol.RoleName, prop => prop.MapFrom(user => user.IdRolNavigation.Name))
                .ForMember(rolePermissions => rolePermissions.RolePermissions, prop => prop.MapFrom(user => user.IdRolNavigation.RoleHasPermissions.Select(x => x.IdPermissionNavigation).ToList()))
                //.ForMember(userPermissions => userPermissions.UserPermissions, prop => prop.MapFrom(user => user.UsersHasPermissions.Select(x => x.IdPermissionNavigation).ToList())) lo comento para hacer la edicion de usuarios
                .ForMember(userPermissions => userPermissions.UserPermissions, prop => prop.MapFrom(user => user.UsersHasPermissions))
                .ReverseMap();
            CreateMap<User, UserPutDTO>()
                .ForMember(user => user.UserPermissions, prop => prop.MapFrom(user => user.UsersHasPermissions))
                .ReverseMap();

            //CreateMap<UsersHasPermission, UsersHasPermissionDTO>().ReverseMap();
            //CreateMap<UsersHasPermission, PermissionDTO>()
            //    .ForMember(permissionsDTO => permissionsDTO.IdPermission,
            //        prop => prop.MapFrom(usersHasPermission => usersHasPermission.IdPermissionNavigation.IdPermission))
            //    .ForMember(permissionsDTO => permissionsDTO.Name,
            //        prop => prop.MapFrom(usersHasPermission => usersHasPermission.IdPermissionNavigation.Name))
            //    .ForMember(permissionsDTO => permissionsDTO.Status,
            //        prop => prop.MapFrom(usersHasPermission => usersHasPermission.IdPermissionNavigation.Status))
            //    .ReverseMap();


            //CreateMap<Vulnerability, VulnerabilityDTO>().ReverseMap();
            //CreateMap<MadurityModel, MadurityModelLevelDTO>().ReverseMap();

            //CreateMap<FrameworkThirdlevel, FrameworkThirdlevelFrameworkDTO>()
            //    .ForMember(frameworkthirdlevelframework => frameworkthirdlevelframework.NameFrameworkFirst, prop => prop.MapFrom(frameworkthirdlevel => frameworkthirdlevel.IdSecondLevelNavigation.IdFirstLevelNavigation.Name))
            //    .ForMember(frameworkthirdlevelframework => frameworkthirdlevelframework.NameFrameworkSecond, prop => prop.MapFrom(frameworkthirdlevel => frameworkthirdlevel.IdSecondLevelNavigation.Name))
            //    .ForMember(frameworkthirdlevelframework => frameworkthirdlevelframework.IdFirstLevel, prop => prop.MapFrom(frameworkthirdlevel => frameworkthirdlevel.IdSecondLevelNavigation.IdFirstLevelNavigation.IdFirstLevel))
            //    .ReverseMap();

            ////Evitar las referencias circulares en frameworks
            //CreateMap<Framework, FrameworkDetailDTO>().ReverseMap();

            //CreateMap<FrameworkFirstlevel, FrameworkFirstlevelDetailDTO>().ReverseMap();

            //CreateMap<FrameworkSecondlevel, FrameworkSecondlevelDetailDTO>().ReverseMap();

            //CreateMap<Framework, FrameworkPostDTO>().ReverseMap();

            //CreateMap<FrameworkFirstlevel, FrameworkFirstLevelPostDTO>().ReverseMap();

            ////Evitar las referencias circulares en frameworks



            ////Evitar las referencias circulares en madurity models

            //CreateMap<MadurityModel, MadurityModelDetailDTO>().ReverseMap();

            ////Evitar las referencias circulares en madurity models


            //CreateMap<GapAnalyze, GapAnalyzeFrameworkDTO>()
            //    .ForMember(gapAnalyzeFramework => gapAnalyzeFramework.Framework, prop => prop.MapFrom(framework => framework.IdFrameworkNavigation.Name))
            //    .ReverseMap();

            //CreateMap<ResultsHasEvidence, ResultHasEvidenceDetailDTO>()
            //    .ForMember(resultHasEvidenceDetailDTO => resultHasEvidenceDetailDTO.Evidence, prop => prop.MapFrom(resultaHasEvidence => resultaHasEvidence.IdEvidenceNavigation))
            //    .ReverseMap();

            //CreateMap<ResultsHasEvidence, ResultsHasEvidenceDTO>()
            //    .ReverseMap();

            //CreateMap<ResultsHasEvidenceDTO, ResultHasEvidenceDetailDTO>()
            //    .ReverseMap();

            CreateMap<VulnerabilityAssessment, VulnerabilityAssessmentMethodologyAscopeDTO>()
                .ForMember(vulnerabilityAssessmentMethodologyAscopeDTO => vulnerabilityAssessmentMethodologyAscopeDTO.Methodologies, prop => prop.MapFrom(VulnerabilityAssessment => VulnerabilityAssessment.VulnerabilityAssessmentHasMethodologies
                .Select(x => x.IdMethodologieNavigation).ToList()))
                .ForMember(vulnerabilityAssessmentMethodologyAscopeDTO => vulnerabilityAssessmentMethodologyAscopeDTO.Scopes, prop => prop.MapFrom(VulnerabilityAssessment => VulnerabilityAssessment.Scopes))
                //----------------
                .ReverseMap()
                ;


            CreateMap<VulnerabilityAssessmentPostDTO, VulnerabilityAssessment>()
                .ForMember(vulnerabilityAssessment => vulnerabilityAssessment.VulnerabilityAssessmentHasMethodologies, prop => prop.MapFrom(VulnerabilityAssessmentMethodologyAscopeDTO => VulnerabilityAssessmentMethodologyAscopeDTO.Methodologies))
                .ForMember(vulnerabilityAssessmentMethodologyAscopeDTO => vulnerabilityAssessmentMethodologyAscopeDTO.Scopes, prop => prop.MapFrom(VulnerabilityAssessment => VulnerabilityAssessment.Scopes))
                .ReverseMap()
                ;
            CreateMap<DetailHasEvidenceDTO, DetailHasEvidenceQueryDTO>()
                .ReverseMap();
        }
    }
}
