

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;


namespace Cats.Services.EarlyWarning
{

    public class RegionalRequestDetailService : IRegionalRequestDetailService
    {
        private readonly IUnitOfWork _unitOfWork;


        public RegionalRequestDetailService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddReliefRequisitionDetail(RegionalRequestDetail reliefRequisitionDetail)
        {
            _unitOfWork.ReliefRequisitionDetailRepository.Add(reliefRequisitionDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool EditReliefRequisitionDetail(RegionalRequestDetail reliefRequisitionDetail)
        {
            _unitOfWork.ReliefRequisitionDetailRepository.Edit(reliefRequisitionDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteReliefRequisitionDetail(RegionalRequestDetail reliefRequisitionDetail)
        {
            if (reliefRequisitionDetail == null) return false;
            _unitOfWork.ReliefRequisitionDetailRepository.Delete(reliefRequisitionDetail);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.ReliefRequisitionDetailRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ReliefRequisitionDetailRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public bool Save()
        {
            _unitOfWork.Save();
            return true;
        }
        public List<RegionalRequestDetail> GetAllReliefRequisitionDetail()
        {
            return _unitOfWork.ReliefRequisitionDetailRepository.GetAll();
        }
        public RegionalRequestDetail FindById(int id)
        {
            return _unitOfWork.ReliefRequisitionDetailRepository.FindById(id);
        }
        public List<RegionalRequestDetail> FindBy(Expression<Func<RegionalRequestDetail, bool>> predicate)
        {
            return _unitOfWork.ReliefRequisitionDetailRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


