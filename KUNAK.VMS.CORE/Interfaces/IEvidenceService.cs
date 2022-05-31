using KUNAK.VMS.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IEvidenceService
    {
        IEnumerable<Evidence> GetEvidences(int idVulnerabilityAssessment);
        Task<Evidence> GetEvidence(int id);
        Task InsertEvidence(Evidence evidence);
        Evidence GetLastEvidence();
        Task UpdateEvidence(Evidence evidence);
        Task<bool> DeleteEvidence(int id);
        IEnumerable<Evidence> GetEvidencesDetail(int idVulnerabilityAssessment);
        Evidence GetEvidenceByName(String fileName, int idVulnerabilityAssessment);
    }
}
