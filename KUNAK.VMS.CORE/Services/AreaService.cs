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
    public class AreaService : IAreaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public AreaService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public IEnumerable<Area> GetAreas(AreaQueryFilter filters)
        {
            var areas = _unitOfWork.AreaRepository.GetAll();
            return areas;
        }
        public Task<Area> GetArea(int id)
        {
            return _unitOfWork.AreaRepository.GetById(id);
        }
        public async Task InsertArea(Area area)
        {
            await _unitOfWork.AreaRepository.Add(area);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateArea(Area area)
        {
            _unitOfWork.AreaRepository.Update(area);
            await _unitOfWork.SaveChangesAsync();
            //----------------------
        }
        public async Task<bool> DeleteArea(int id)
        {
            await _unitOfWork.AreaRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
