using Azure.Storage.Blobs;
using FluentValidation.AspNetCore;
using KUNAK.VMS.API.Interfaces;
using KUNAK.VMS.API.Methods;
using KUNAK.VMS.CORE.CustomEntities;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.CORE.Services;
using KUNAK.VMS.INFRASTRUCTURE.Data;
using KUNAK.VMS.INFRASTRUCTURE.Filters;
using KUNAK.VMS.INFRASTRUCTURE.Interfaces;
using KUNAK.VMS.INFRASTRUCTURE.Repositories;
using KUNAK.VMS.INFRASTRUCTURE.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//-----------------------------------------------------------------------------------------------

//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Avoid cycling references
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    //options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://kunak-vms-back-test.azurewebsites.net/")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

//Swagger documentation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "KUNAK.VMS.API", Version = "v1" });
});

//configuration pagination options

builder.Services.Configure<PaginationOptions>(builder.Configuration.GetSection("Pagination"));

//db conection
builder.Services.AddDbContext<VMSContext>(x => 
    x.UseSqlServer(builder.Configuration.GetConnectionString("gap_db")));
builder.Services.AddSingleton(new BlobServiceClient(builder.Configuration.GetConnectionString("azure_storage")));

//Interfaces instances
builder.Services.AddTransient<IAditionalDetailService, AditionalDetailService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICompanyService, CompanyService>(); 
builder.Services.AddTransient<IDetailHasEvidenceService, DetailHasEvidenceService>(); 
 builder.Services.AddTransient<IConsultantHasVulnerabilityAssessmentService, ConsultantHasVulnerabilityAssessmentService>(); 
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IRoleHasPermissionService, RoleHasPermissionService>();
builder.Services.AddTransient<IPermissionService, PermissionService>();
builder.Services.AddTransient<IUsersHasPermissionService, UsersHasPermissionService>();
builder.Services.AddTransient<IAssetService, AssetService>();
builder.Services.AddTransient<IAssetTypeService, AssetTypeService>();
builder.Services.AddTransient<ICriticalityService, CriticalityService>(); 
builder.Services.AddTransient<IVulnerabilityStateService, VulnerabilityStateService>(); 
builder.Services.AddTransient<IMethodologyService, MethodologyService>();
builder.Services.AddTransient<IProviderService, ProviderService>();
builder.Services.AddTransient<IScopeService, ScopeService>();
builder.Services.AddTransient<IScopeDetailService, ScopeDetailService>();
builder.Services.AddTransient<IAreaService, AreaService>(); 
builder.Services.AddTransient<IVulnerabilityAssessmentService, VulnerabilityAssessmentService>(); 
builder.Services.AddTransient<IVulnerabilityAssessmentDetailService, VulnerabilityAssessmentDetailService>(); 
builder.Services.AddTransient<IVulnerabilityAssessmentHasMethodologyService, VulnerabilityAssessmentHasMethodologyService>(); 
builder.Services.AddTransient<IBlacklistPasswordService, BlacklistPasswordService>(); 
builder.Services.AddTransient<IPersonService, PersonService>(); 
builder.Services.AddTransient<IEvidenceService, EvidenceService>();

//We are using a Generic Repository for some entities
builder.Services.AddTransient<IAditionalDetailRepository, AditionalDetailRepository>(); 
builder.Services.AddTransient<IUserRepository, UserRepository>(); 
 builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
builder.Services.AddTransient<IDetailHasEvidenceRepository, DetailHasEvidenceRepository>();
builder.Services.AddTransient<IConsultantHasVulnerabilityAssessmentRepository, ConsultantHasVulnerabilityAssessmentRepository>(); 
 builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IRoleHasPermissionRepository, RoleHasPermissionRepository>();
builder.Services.AddTransient<IPermissionRepository, PermissionRepository>();
builder.Services.AddTransient<IUsersHasPermissionRepository, UsersHasPermissionRepository>();
builder.Services.AddTransient<IAssetRepository, AssetRepository>();
builder.Services.AddTransient<IAssetTypeRepository, AssetTypeRepository>();
builder.Services.AddTransient<ICriticalityRepository, CriticalityRepository>();
builder.Services.AddTransient<IMethodologyRepository, MethodologyRepository>();
builder.Services.AddTransient<IVulnerabilityStateRepository, VulnerabilityStateRepository>();  
builder.Services.AddTransient<IProviderRepository, ProviderRepository>(); 
builder.Services.AddTransient<IScopeRepository, ScopeRepository>();
builder.Services.AddTransient<IScopeDetailRepository, ScopeDetailRepository>();
builder.Services.AddTransient<IAreaRepository, AreaRepository>(); 
builder.Services.AddTransient<IVulnerabilityAssessmentRepository, VulnerabilityAssessmentRepository>(); 
builder.Services.AddTransient<IVulnerabilityAssessmentDetailRepository, VulnerabilityAssessmentDetailRepository>(); 
builder.Services.AddTransient<IVulnerabilityAssessmentHasMethodologyRepository, VulnerabilityAssessmentHasMethodologyRepository>(); 
 builder.Services.AddTransient<IBlacklistPasswordRepository, BlacklistPasswordRepository>(); 
builder.Services.AddTransient<IPersonRepository, PersonRepository>(); 
 //--
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IValidateUserPermissions), typeof(ValidateUserPermissions));
builder.Services.AddScoped(typeof(IBlobManagement), typeof(BlobManagement));
builder.Services.AddScoped(typeof(IReport), typeof(Report));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddSingleton<IUriService>(provider =>
{
    var accesor = provider.GetRequiredService<IHttpContextAccessor>();
    var request = accesor.HttpContext.Request;
    var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new UriServices(absoluteUri);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]))
    };
});
//JWT ADD BEFORE MVC
builder.Services.AddMvc(options =>
{
    //options.Filters.Add<ValidationFilter>();
}).AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});


//-----------------------------------------------------------------------------------------------

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KUNAK.VMS.API v1"));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
