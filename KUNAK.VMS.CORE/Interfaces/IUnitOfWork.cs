using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        //IRepository<Company> CompanyRepository { get; }
        IAditionalDetailRepository AditionalDetailRepository { get; }
        IAreaRepository AreaRepository { get; }
        IAssetRepository AssetRepository { get; }
        IAssetTypeRepository AssetTypeRepository { get; }
        ICriticalityRepository CriticalityRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        IConsultantHasVulnerabilityAssessmentRepository ConsultantHasVulnerabilityAssessmentRepository { get; }
        IDetailHasEvidenceRepository DetailHasEvidenceRepository { get; }
        IEvidenceRepository EvidenceRepository { get; }
        IProviderRepository ProviderRepository { get; }
        IScopeRepository ScopeRepository { get; }
        IScopeDetailRepository ScopeDetailRepository { get; }
        //IMadurityLevelRepository MadurityLevelRepository { get; }
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IPermissionRepository PermissionRepository { get; }
        IVulnerabilityStateRepository VulnerabilityStateRepository { get; }
        IMethodologyRepository MethodologyRepository { get; }
        //IFrameworkFirstlevelRepository FrameworkFirstlevelRepository { get; }
        //IFrameworkSecondlevelRepository FrameworkSecondlevelRepository { get; }
        //IFrameworkThirdlevelRepository FrameworkThirdlevelRepository { get; }
        //IGapAnalyzeRepository GapAnalyzeRepository { get; }
        //IProjectRepository ProjectRepository { get; }
        //IActivityRepository ActivityRepository { get; }
        //IGapControlResultRepository GapControlResultRepository { get; }
        //ICompanyHasFrameworkRepository CompanyHasFrameworkRepository { get; }
        //IResultsHasEvidenceRepository ResultsHasEvidenceRepository { get; }
        IRoleHasPermissionRepository RoleHasPermissionRepository { get; }
        IUsersHasPermissionRepository UsersHasPermissionRepository { get; }
        IVulnerabilityAssessmentRepository VulnerabilityAssessmentRepository { get; }
        IVulnerabilityAssessmentDetailRepository VulnerabilityAssessmentDetailRepository { get; }
        IVulnerabilityAssessmentHasMethodologyRepository VulnerabilityAssessmentHasMethodologyRepository { get; }
        IBlacklistPasswordRepository BlacklistPasswordRepository { get; }
        IPersonRepository PersonRepository { get; }
        //IProjectHasGapResultRepository ProjectHasGapResultRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
