using KUNAK.VMS.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.INFRASTRUCTURE.Interfaces
{
    public interface IUriService
    {
        Uri GetAssetPaginationUri(AssetQueryFilter filter, string actionUrl);
        Uri GetAssetTypePaginationUri(AssetTypeQueryFilter filter, string actionUrl);
        Uri GetCriticalityPaginationUri(CriticalityQueryFilter filter, string actionUrl); 
        Uri GetConsultantHasVulnerabilityAssessmentPaginationUri(ConsultantHasVulnerabilityAssessmentQueryFilter filter, string actionUrl); 
         //Uri GetCompanyPaginationUri(CompanyQueryFilter filter, string actionUrl);
         //Uri GetMadurityModelPaginationUri(MadurityModelQueryFilter filter, string actionUrl);
         //Uri GetMadurityLevelPaginationUri(MadurityLevelQueryFilter filter, string actionUrl);
         Uri GetUserPaginationUri(UserQueryFilter filter, string actionUrl);
        //Uri GetFrameworkPaginationUri(FrameworkQueryFilter filter, string actionUrl);
        //Uri GetFrameworkThirdlevelPaginationUri(FrameworkThirdlevelQueryFilter filter, string actionUrl);
        //Uri GetGapAnalyzePaginationUri(GapAnalyzeQueryFilter filter, string actionUrl);
        //Uri GetGapControlResultPaginationUri(GapControlResultQueryFilter filter, string actionUrl);
        //Uri GetProjectPaginationUri(ProjectQueryFilter filter, string actionUrl);
        //Uri GetActivityPaginationUri(ActivityQueryFilter filter, string actionUrl);
        Uri GetComplianceStatePaginationUri(ComplianceStateQueryFilter filter, string actionUrl); 
        Uri GetCvssStatePaginationUri(CvssStateQueryFilter filter, string actionUrl);
        Uri GetVulnerabilityStatePaginationUri(VulnerabilityStateQueryFilter filter, string actionUrl); 
        Uri GetVulnerabilityAssessmentPaginationUri(VulnerabilityAssessmentQueryFilter filter, string actionUrl); 
        Uri GetVulnerabilityAssessmentDetailPaginationUri(VulnerabilityAssessmentDetailQueryFilter filter, string actionUrl); 


    }
}
