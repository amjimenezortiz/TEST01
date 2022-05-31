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
    public class AssetTypeService:IAssetTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public AssetTypeService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public IEnumerable<AssetType> GetAssetTypes(AssetTypeQueryFilter filters)
        {
           
            var assetTypes = _unitOfWork.AssetTypeRepository.GetAll();
            return assetTypes;
        }
        public Task<AssetType> GetAssetType(int id)
        {
            return _unitOfWork.AssetTypeRepository.GetById(id);
        }
        public async Task InsertAssetType(AssetType assetType)
        {
            ////Validate if company RUC was registered before
            //var companyValidate = await _unitOfWork.CompanyRepository.GetByRuc(company.Ruc);
            //if (companyValidate != null)
            //{
            //    throw new BusinessException("El RUC ya se encuentra registrado");
            //}
            await _unitOfWork.AssetTypeRepository.Add(assetType);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateAssetType(AssetType assetType)
        {
            ////Validate if company RUC was registered before
            //var companyValidate = await _unitOfWork.CompanyRepository.GetByRuc(company.Ruc);
            //// Se debe verificar que si es la misma empresa, debe permitir ingreasr el mismo ruc
            //// It must be verified that if it is the same company, it must allow entering the same ruc
            //if (companyValidate != null && company.IdCompany != companyValidate.IdCompany)
            //{
            //    throw new BusinessException("El RUC ya se encuentra registrado");
            //}
            _unitOfWork.AssetTypeRepository.Update(assetType);
            await _unitOfWork.SaveChangesAsync();
            //----------------------
        }
        public async Task<bool> DeleteAssetType(int id)
        {
            await _unitOfWork.AssetTypeRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
