

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;


namespace Cats.Services.Logistics
{

    public class DistributionDetailService : IDistributionDetailService
    {
        private readonly IUnitOfWork _unitOfWork;


        public DistributionDetailService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddDistributionDetail(DistributionDetail distributionDetail)
        {
            _unitOfWork.DistributionDetailRepository.Add(distributionDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool EditDistributionDetail(DistributionDetail distributionDetail)
        {
            _unitOfWork.DistributionDetailRepository.Edit(distributionDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteDistributionDetail(DistributionDetail distributionDetail)
        {
            if (distributionDetail == null) return false;
            _unitOfWork.DistributionDetailRepository.Delete(distributionDetail);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.DistributionDetailRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.DistributionDetailRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<DistributionDetail> GetAllDistributionDetail()
        {
            return _unitOfWork.DistributionDetailRepository.GetAll();
        }
        public DistributionDetail FindById(int id)
        {
            return _unitOfWork.DistributionDetailRepository.FindById(id);
        }
        public List<DistributionDetail> FindBy(Expression<Func<DistributionDetail, bool>> predicate)
        {
            return _unitOfWork.DistributionDetailRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


