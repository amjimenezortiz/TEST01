using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Interfaces
{
    public interface IDetailHasEvidenceService
    {
        IEnumerable<DetailHasEvidence> GetDetailHasEvidences(DetailHasEvidenceQueryFilter filters);
        Task<DetailHasEvidence> GetDetailHasEvidence(int id);
        Task InsertDetailHasEvidence(DetailHasEvidence detailHasEvidence);
        Task UpdateDetailHasEvidence(DetailHasEvidence detailHasEvidence);
        Task<bool> DeleteDetailHasEvidence(int idVulnerabilityAssessmentDetail, int idEvidence);
    }
}
