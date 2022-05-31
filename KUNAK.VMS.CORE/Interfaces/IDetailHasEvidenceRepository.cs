using KUNAK.VMS.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IDetailHasEvidenceRepository:IRepository<DetailHasEvidence>
    {
        void DeleteByIdConcatenated(int idVulnerabilityAssessmentDetail, int idEvidence);
        DetailHasEvidence GetByIdConcatenated(int idVulnerabilityAssessmentDetail, int idEvidence);
        IEnumerable<DetailHasEvidence> GetByEvidence(int idEvidence);
    }
}
