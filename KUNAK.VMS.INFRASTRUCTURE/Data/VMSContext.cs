using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.INFRASTRUCTURE.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KUNAK.VMS.INFRASTRUCTURE.Data
{
    public partial class VMSContext : DbContext
    {
        public VMSContext()
        {
        }

        public VMSContext(DbContextOptions<VMSContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<AditionalDetail> AditionalDetails { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<AssetType> AssetTypes { get; set; }
        public virtual DbSet<BlacklistPassword> BlacklistPasswords { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<ConsultantHasVulnerabilityAssessment> ConsultantHasVulnerabilityAssessments { get; set; }
        public virtual DbSet<Criticality> Criticalities { get; set; }
        public virtual DbSet<CvssDetail> CvssDetails { get; set; }
        public virtual DbSet<DataScope> DataScopes { get; set; }
        public virtual DbSet<DetailHasEvidence> DetailHasEvidences { get; set; }
        public virtual DbSet<Evidence> Evidences { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Methodology> Methodologies { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleHasPermission> RoleHasPermissions { get; set; }
        public virtual DbSet<Scope> Scopes { get; set; }
        public virtual DbSet<ScopeDetail> ScopeDetails { get; set; }
        public virtual DbSet<Treatment> Treatments { get; set; }
        public virtual DbSet<TreatmentEvidence> TreatmentEvidences { get; set; }
        public virtual DbSet<TreatmentHasState> TreatmentHasStates { get; set; }
        public virtual DbSet<TreatmentState> TreatmentStates { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersHasPermission> UsersHasPermissions { get; set; }
        public virtual DbSet<VulAssementDetailHasAsset> VulAssementDetailHasAssets { get; set; }
        public virtual DbSet<VulAssessmentDetHasTreatment> VulAssessmentDetHasTreatments { get; set; }
        public virtual DbSet<VulnerabilityAssementDetailStatus> VulnerabilityAssementDetailStatuses { get; set; }
        public virtual DbSet<VulnerabilityAssessment> VulnerabilityAssessments { get; set; }
        public virtual DbSet<VulnerabilityAssessmentDetail> VulnerabilityAssessmentDetails { get; set; }
        public virtual DbSet<VulnerabilityAssessmentHasMethodology> VulnerabilityAssessmentHasMethodologies { get; set; }
        public virtual DbSet<VulnerabilityState> VulnerabilityStates { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("Server=.;Database=VMS;Integrated Security = true");
        //            }
        //        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("gap_db");
            optionsBuilder.UseSqlServer(connectionString);
        }

        //-----------------------------------------
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.ApplyConfiguration(new ActivityConfiguration());

            modelBuilder.ApplyConfiguration(new AditionalDetailConfiguration());

            modelBuilder.ApplyConfiguration(new AreaConfiguration());

            modelBuilder.ApplyConfiguration(new AssetConfiguration());

            modelBuilder.ApplyConfiguration(new AssetTypeConfiguration());

            modelBuilder.ApplyConfiguration(new BlacklistPasswordConfiguration());

            modelBuilder.ApplyConfiguration(new CompanyConfiguration());

            modelBuilder.ApplyConfiguration(new ConsultantHasVulnerabilityAssessmentConfiguration());

            modelBuilder.ApplyConfiguration(new CriticalityConfiguration());

            modelBuilder.ApplyConfiguration(new CvssDetailConfiguration());

            modelBuilder.ApplyConfiguration(new DataScopeConfiguration());

            modelBuilder.ApplyConfiguration(new DetailHasEvidenceConfiguration());

            modelBuilder.ApplyConfiguration(new EvidenceConfiguration());

            modelBuilder.ApplyConfiguration(new LogConfiguration());
            
            modelBuilder.ApplyConfiguration(new MethodologyConfiguration());

            modelBuilder.ApplyConfiguration(new PermissionConfiguration());

            modelBuilder.ApplyConfiguration(new PersonConfiguration());

            modelBuilder.ApplyConfiguration(new ProviderConfiguration());

            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.ApplyConfiguration(new RoleHasPermissionConfiguration());

            modelBuilder.ApplyConfiguration(new ScopeConfiguration());

            modelBuilder.ApplyConfiguration(new ScopeDetailConfiguration());

            modelBuilder.ApplyConfiguration(new TreatmentConfiguration());

            modelBuilder.ApplyConfiguration(new TreatmentEvidenceConfiguration());

            modelBuilder.ApplyConfiguration(new TreatmentHasStateConfiguration());

            modelBuilder.ApplyConfiguration(new TreatmentStateConfiguration());

            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new UsersHasPermissionConfiguration());

            modelBuilder.ApplyConfiguration(new VulAssementDetailHasAssetConfiguration()); 

            modelBuilder.ApplyConfiguration(new VulAssessmentDetHasTreatmentConfiguration()); 

            modelBuilder.ApplyConfiguration(new VulnerabilityAssementDetailStatusConfiguration());

            modelBuilder.ApplyConfiguration(new VulnerabilityAssessmentConfiguration());

            modelBuilder.ApplyConfiguration(new VulnerabilityAssessmentDetailConfiguration()); 

            modelBuilder.ApplyConfiguration(new VulnerabilityAssessmentHasMethodologyConfiguration()); 

            modelBuilder.ApplyConfiguration(new VulnerabilityStateConfiguration());

            //OnModelCreatingPartial(modelBuilder);
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
