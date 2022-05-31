using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.INFRASTRUCTURE.Data;
using KUNAK.VMS.INFRASTRUCTURE.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KUNAK.VMS.INFRASTRUCTURE.Repositories
{
    public class EvidenceRepository : BaseRepository<Evidence>, IEvidenceRepository
    {
        public EvidenceRepository(VMSContext context) : base(context) { }
        public IEnumerable<Evidence> GetEvidencesByVulnerabilityAssessment(int idVulnerabilityAssessment)
        {
            return _entities.Where(x => x.IdVulnerabilityAssessment == idVulnerabilityAssessment).AsEnumerable();
        }
        public Evidence GetLastEvidence()
        {
            return _entities.OrderBy(x => x.IdEvidence).LastOrDefault();
        }

        //Crud
        public IEnumerable<Evidence> GetEvidencesDetail(int idVulnerabilityAssessment)
        {
            return _entities.Include(x => x.DetailHasEvidences)
                .ThenInclude(x => x.IdVulnerabilityAssessmentDetailNavigation)
                .Where(x => x.IdVulnerabilityAssessment == idVulnerabilityAssessment).AsEnumerable();
        }



        public Evidence GetEvidenceByName(string fileName, int idVulnerabilityAssessment)
        {
            return _entities.FirstOrDefault(x => x.Filename == fileName 
                                            && x.IdVulnerabilityAssessment == idVulnerabilityAssessment);
        }
    }
}
