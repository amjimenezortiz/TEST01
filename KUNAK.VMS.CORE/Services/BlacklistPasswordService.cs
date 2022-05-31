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
    public class BlacklistPasswordService : IBlacklistPasswordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public BlacklistPasswordService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public IEnumerable<BlacklistPassword> GetBlacklistPasswords(BlacklistPasswordQueryFilter filters)
        {
            var blacklistPasswords = _unitOfWork.BlacklistPasswordRepository.GetAll();

            return blacklistPasswords;
        }
        public Task<BlacklistPassword> GetBlacklistPassword(int id)
        {
            return _unitOfWork.BlacklistPasswordRepository.GetById(id);
        }
        public async Task InsertBlacklistPassword(BlacklistPassword blacklistPassword)
        {
            await _unitOfWork.BlacklistPasswordRepository.Add(blacklistPassword);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateBlacklistPassword(BlacklistPassword blacklistPassword)
        {
            _unitOfWork.BlacklistPasswordRepository.Update(blacklistPassword);
            await _unitOfWork.SaveChangesAsync();
            //----------------------
        }
        public async Task<bool> DeleteBlacklistPassword(int id)
        {
            await _unitOfWork.BlacklistPasswordRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
