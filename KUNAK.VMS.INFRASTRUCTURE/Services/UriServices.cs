using KUNAK.VMS.CORE.QueryFilters;
using KUNAK.VMS.INFRASTRUCTURE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.INFRASTRUCTURE.Services
{
    public class UriServices : IUriService
    {
        private readonly string _baseUri;
        public UriServices(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetAssetPaginationUri(AssetQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }

        public Uri GetAssetTypePaginationUri(AssetTypeQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }

        public Uri GetCriticalityPaginationUri(CriticalityQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }

        public Uri GetConsultantHasVulnerabilityAssessmentPaginationUri(ConsultantHasVulnerabilityAssessmentQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }

        //public Uri GetMadurityModelPaginationUri(MadurityModelQueryFilter filter, string actionUrl)
        //{
        //    string baseUrl = $"{_baseUri}{actionUrl}";
        //    return new Uri(baseUrl);
        //}
        //public Uri GetMadurityLevelPaginationUri(MadurityLevelQueryFilter filter, string actionUrl)
        //{
        //    string baseUrl = $"{_baseUri}{actionUrl}";
        //    return new Uri(baseUrl);
        //}

        public Uri GetUserPaginationUri(UserQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
        //public Uri GetFrameworkPaginationUri(FrameworkQueryFilter filter, string actionUrl)
        //{
        //    string baseUrl = $"{_baseUri}{actionUrl}";
        //    return new Uri(baseUrl);
        //}
        //public Uri GetFrameworkThirdlevelPaginationUri(FrameworkThirdlevelQueryFilter filter, string actionUrl)
        //{
        //    string baseUrl = $"{_baseUri}{actionUrl}";
        //    return new Uri(baseUrl);
        //}
        //public Uri GetGapAnalyzePaginationUri(GapAnalyzeQueryFilter filter, string actionUrl)
        //{
        //    string baseUrl = $"{_baseUri}{actionUrl}";
        //    return new Uri(baseUrl);
        //}
        //public Uri GetGapControlResultPaginationUri(GapControlResultQueryFilter filter, string actionUrl)
        //{
        //    string baseUrl = $"{_baseUri}{actionUrl}";
        //    return new Uri(baseUrl);
        //}

        //public Uri GetProjectPaginationUri(ProjectQueryFilter filter, string actionUrl)
        //{
        //    string baseUrl = $"{_baseUri}{actionUrl}";
        //    return new Uri(baseUrl);
        //}
        //public Uri GetActivityPaginationUri(ActivityQueryFilter filter, string actionUrl)
        //{
        //    string baseUrl = $"{_baseUri}{actionUrl}";
        //    return new Uri(baseUrl);
        //}

        public Uri GetComplianceStatePaginationUri(ComplianceStateQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
        public Uri GetCvssStatePaginationUri(CvssStateQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }

        public Uri GetVulnerabilityStatePaginationUri(VulnerabilityStateQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }

        
        public Uri GetVulnerabilityAssessmentPaginationUri(VulnerabilityAssessmentQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
        public Uri GetVulnerabilityAssessmentDetailPaginationUri(VulnerabilityAssessmentDetailQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
    }
}
