using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.INFRASTRUCTURE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.INFRASTRUCTURE.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VMSContext _context;
        //In case we only use the base entity class
        //private readonly IRepository<Company> _companyRepository;
        
        
        private readonly IAditionalDetailRepository _aditionalDetailRepository; 
        private readonly IAreaRepository _areaRepository; 
        private readonly IAssetRepository _assetRepository;
        private readonly IAssetTypeRepository _assetTypeRepository;
        private readonly ICriticalityRepository _criticalityRepository; 
        private readonly ICompanyRepository _companyRepository; 
        private readonly IConsultantHasVulnerabilityAssessmentRepository _consultantHasVulnerabilityAssessmentRepository; 
        private readonly IDetailHasEvidenceRepository _detailHasEvidenceRepository;
        private readonly IEvidenceRepository _evidenceRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IScopeRepository _scopeRepository;
        private readonly IScopeDetailRepository _scopeDetailRepository;
        //private readonly IMadurityLevelRepository _madurityLevelRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository; 
        private readonly IVulnerabilityStateRepository _vulnerabilityStateRepository; 
        private readonly IMethodologyRepository _methodologyRepository;
        //private readonly IFrameworkFirstlevelRepository _frameworkFirstlevelRepository;
        //private readonly IFrameworkSecondlevelRepository _frameworkSecondlevelRepository;
        //private readonly IFrameworkThirdlevelRepository _frameworkThirdlevelRepository;
        //private readonly IGapAnalyzeRepository _gapAnalyzeRepository;
        //private readonly IProjectRepository _projectRepository;
        //private readonly IActivityRepository _activityRepository;
        //private readonly IGapControlResultRepository _gapControlResultRepository;
        //private readonly ICompanyHasFrameworkRepository _companyHasFrameworkRepository;
        //private readonly IResultsHasEvidenceRepository _resultsHasEvidenceRepository;
        private readonly IRoleHasPermissionRepository _roleHasPermissionRepository;
        private readonly IUsersHasPermissionRepository _usersHasPermissionRepository; 
        private readonly IVulnerabilityAssessmentRepository _vulnerabilityAssessmentRepository; 
        private readonly IVulnerabilityAssessmentDetailRepository _vulnerabilityAssessmentDetailRepository; 
        private readonly IVulnerabilityAssessmentHasMethodologyRepository _vulnerabilityAssessmentHasMethodologyRepository;
        private readonly IBlacklistPasswordRepository _blacklistPasswordRepository; 
        private readonly IPersonRepository _personRepository; 
        //private readonly IProjectHasGapResultRepository _projectHasGapResultRepository;

        public UnitOfWork(VMSContext context)
        {
            _context = context;
        }
        //In case we only use the base entity class
        //public IRepository<Company> CompanyRepository => _companyRepository ?? new BaseRepository<Company>(_context);


        public IAditionalDetailRepository AditionalDetailRepository => _aditionalDetailRepository ?? new AditionalDetailRepository(_context); 
        public IAreaRepository AreaRepository => _areaRepository ?? new AreaRepository(_context); 
        public IAssetRepository AssetRepository => _assetRepository ?? new AssetRepository(_context);
        public IAssetTypeRepository AssetTypeRepository => _assetTypeRepository ?? new AssetTypeRepository(_context);
        public ICriticalityRepository CriticalityRepository => _criticalityRepository ?? new CriticalityRepository(_context);
        public ICompanyRepository CompanyRepository => _companyRepository ?? new CompanyRepository(_context); 
        public IConsultantHasVulnerabilityAssessmentRepository ConsultantHasVulnerabilityAssessmentRepository => _consultantHasVulnerabilityAssessmentRepository ?? new ConsultantHasVulnerabilityAssessmentRepository(_context); 
        public IEvidenceRepository EvidenceRepository => _evidenceRepository ?? new EvidenceRepository(_context); 
        public IDetailHasEvidenceRepository DetailHasEvidenceRepository => _detailHasEvidenceRepository ?? new DetailHasEvidenceRepository(_context); 
        public IProviderRepository ProviderRepository => _providerRepository ?? new ProviderRepository(_context);
        public IScopeRepository ScopeRepository => _scopeRepository ?? new ScopeRepository(_context);
        public IScopeDetailRepository ScopeDetailRepository => _scopeDetailRepository ?? new ScopeDetailRepository(_context);
        //public IMadurityLevelRepository MadurityLevelRepository => _madurityLevelRepository ?? new MadurityLevelRepository(_context);
        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);
        public IRoleRepository RoleRepository => _roleRepository ?? new RoleRepository(_context);
        public IPermissionRepository PermissionRepository => _permissionRepository ?? new PermissionRepository(_context); 
        //public IFrameworkRepository FrameworkRepository => _frameworkRepository ?? new FrameworkRepository(_context);
        public IVulnerabilityStateRepository VulnerabilityStateRepository => _vulnerabilityStateRepository ?? new VulnerabilityStateRepository(_context); 
        public IMethodologyRepository MethodologyRepository => _methodologyRepository ?? new MethodologyRepository(_context);
        //public IFrameworkFirstlevelRepository FrameworkFirstlevelRepository => _frameworkFirstlevelRepository ?? new FrameworkFirstlevelRepository(_context);
        //public IFrameworkSecondlevelRepository FrameworkSecondlevelRepository => _frameworkSecondlevelRepository ?? new FrameworkSecondlevelRepository(_context);
        //public IFrameworkThirdlevelRepository FrameworkThirdlevelRepository => _frameworkThirdlevelRepository ?? new FrameworkThirdlevelRepository(_context);
        //public IGapAnalyzeRepository GapAnalyzeRepository => _gapAnalyzeRepository ?? new GapAnalyzeRepository(_context);
        //public IProjectRepository ProjectRepository => _projectRepository ?? new ProjectRepository(_context);
        //public IActivityRepository ActivityRepository => _activityRepository ?? new ActivityRepository(_context);
        //public IGapControlResultRepository GapControlResultRepository => _gapControlResultRepository ?? new GapControlResultRepository(_context);
        //public ICompanyHasFrameworkRepository CompanyHasFrameworkRepository => _companyHasFrameworkRepository ?? new CompanyHasFrameworkRepository(_context);
        //public IResultsHasEvidenceRepository ResultsHasEvidenceRepository => _resultsHasEvidenceRepository ?? new ResultsHasEvidenceRepository(_context);
        public IRoleHasPermissionRepository RoleHasPermissionRepository => _roleHasPermissionRepository ?? new RoleHasPermissionRepository(_context);
        public IUsersHasPermissionRepository UsersHasPermissionRepository => _usersHasPermissionRepository ?? new UsersHasPermissionRepository(_context);
        public IVulnerabilityAssessmentRepository VulnerabilityAssessmentRepository => _vulnerabilityAssessmentRepository ?? new VulnerabilityAssessmentRepository(_context); 
        public IVulnerabilityAssessmentDetailRepository VulnerabilityAssessmentDetailRepository => _vulnerabilityAssessmentDetailRepository ?? new VulnerabilityAssessmentDetailRepository(_context); 
        public IVulnerabilityAssessmentHasMethodologyRepository VulnerabilityAssessmentHasMethodologyRepository => _vulnerabilityAssessmentHasMethodologyRepository ?? new VulnerabilityAssessmentHasMethodologyRepository(_context); 
        public IBlacklistPasswordRepository BlacklistPasswordRepository => _blacklistPasswordRepository ?? new BlacklistPasswordRepository(_context); 
        public IPersonRepository PersonRepository => _personRepository ?? new PersonRepository(_context); 

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
