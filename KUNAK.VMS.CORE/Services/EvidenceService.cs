using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Exceptions;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Services
{
    public class EvidenceService : IEvidenceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EvidenceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Evidence> GetEvidences(int idVulnerabilityAssessment)
        {
            return _unitOfWork.EvidenceRepository.GetEvidencesByVulnerabilityAssessment(idVulnerabilityAssessment);
        }
        public async Task<Evidence> GetEvidence(int id)
        {
            return await _unitOfWork.EvidenceRepository.GetById(id);
        }
        public async Task InsertEvidence(Evidence evidence)
        {
            await _unitOfWork.EvidenceRepository.Add(evidence);
            await _unitOfWork.SaveChangesAsync();
        }

        public Evidence GetLastEvidence()
        {
            return _unitOfWork.EvidenceRepository.GetLastEvidence();
        }

        public async Task UpdateEvidence(Evidence evidence)
        {

            _unitOfWork.EvidenceRepository.Update(evidence);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task<bool> DeleteEvidence(int id)
        {

            //Eliminamos todas las relaciones de las evicencias
            //We remove all relationships from evidence
            var detailHasEvidences = _unitOfWork.DetailHasEvidenceRepository.GetByEvidence(id).ToList();
            foreach (var detailHasEvidence in detailHasEvidences)
            {
                _unitOfWork.DetailHasEvidenceRepository.DeleteByIdConcatenated(
                        detailHasEvidence.IdVulnerabilityAssessmentDetail,
                        detailHasEvidence.IdEvidence
                );
                await _unitOfWork.SaveChangesAsync();
            }
            //Eliminamos la evidencia
            //We remove the evidence
            await _unitOfWork.EvidenceRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        //----------------
        public IEnumerable<Evidence> GetEvidencesDetail(int idVulnerabilityAssessment)
        {
            var evidences = _unitOfWork.EvidenceRepository.GetEvidencesDetail(idVulnerabilityAssessment);
            evidences = evidences.OrderBy(x => x.Filename);
            return evidences;
        }

        public Evidence GetEvidenceByName(String fileName, int idVulnerabilityAssessment)
        {
            return _unitOfWork.EvidenceRepository.GetEvidenceByName(fileName, idVulnerabilityAssessment);
        }
    }
}
