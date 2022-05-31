using KUNAK.VMS.CORE.CustomEntities;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.CORE.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Services
{
    public class DetailHasEvidenceService:IDetailHasEvidenceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public DetailHasEvidenceService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public IEnumerable<DetailHasEvidence> GetDetailHasEvidences(DetailHasEvidenceQueryFilter filters)
        {
            var detailHasEvidences = _unitOfWork.DetailHasEvidenceRepository.GetAll();
            return detailHasEvidences;
        }
        public Task<DetailHasEvidence> GetDetailHasEvidence(int id)
        {
            return _unitOfWork.DetailHasEvidenceRepository.GetById(id);
        }
        public async Task InsertDetailHasEvidence(DetailHasEvidence detailHasEvidence)
        {
            await _unitOfWork.DetailHasEvidenceRepository.Add(detailHasEvidence);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateDetailHasEvidence(DetailHasEvidence detailHasEvidence)
        {
            _unitOfWork.DetailHasEvidenceRepository.Update(detailHasEvidence);
            await _unitOfWork.SaveChangesAsync();
            //----------------------
        }
        public async Task<bool> DeleteDetailHasEvidence(int idVulnerabilityAssessmentDetail,int idEvidence)
        {
            _unitOfWork.DetailHasEvidenceRepository.DeleteByIdConcatenated(idVulnerabilityAssessmentDetail, idEvidence);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
