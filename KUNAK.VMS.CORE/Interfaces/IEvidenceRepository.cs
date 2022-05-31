using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IEvidenceRepository : IRepository<Evidence>
    {
        IEnumerable<Evidence> GetEvidencesByVulnerabilityAssessment(int idVulnerabilityAssessment);
        Evidence GetLastEvidence();
        IEnumerable<Evidence> GetEvidencesDetail(int idVulnerabilityAssessment);
        Evidence GetEvidenceByName(string fileName, int idVulnerabilityAssessment);
    }
}
