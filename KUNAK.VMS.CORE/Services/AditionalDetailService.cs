using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUNAK.VMS.CORE.Services
{
    public class AditionalDetailService: IAditionalDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AditionalDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task InsertAditionalDetail(AditionalDetail AditionalDetail)
        {
            await _unitOfWork.AditionalDetailRepository.Add(AditionalDetail);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
