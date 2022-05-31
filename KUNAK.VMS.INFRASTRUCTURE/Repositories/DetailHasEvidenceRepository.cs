using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.INFRASTRUCTURE.Repositories
{
    public class DetailHasEvidenceRepository : BaseRepository<DetailHasEvidence>, IDetailHasEvidenceRepository
    {
        public DetailHasEvidenceRepository(VMSContext context):base(context){}
        public void DeleteByIdConcatenated(int idVulnerabilityAssessmentDetail, int idEvidence)
        {
            DetailHasEvidence entity = GetByIdConcatenated(idVulnerabilityAssessmentDetail,idEvidence);
            _entities.Remove(entity);
        }
        public DetailHasEvidence GetByIdConcatenated(int idVulnerabilityAssessmentDetail, int idEvidence)
        {
            return _entities.AsNoTracking()
                .Where(x => x.IdVulnerabilityAssessmentDetail == idVulnerabilityAssessmentDetail)
                .Where(x => x.IdEvidence == idEvidence).FirstOrDefault();
        }
        public IEnumerable<DetailHasEvidence> GetByEvidence(int idEvidence)
        {
            return _entities.AsNoTracking()
                .Where(x => x.IdEvidence == idEvidence)
                .AsEnumerable();
        }
    }
}
