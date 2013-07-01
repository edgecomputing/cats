

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;


namespace Cats.Services.EarlyWarning
{

    public class ReliefRequisitionDetailService : IReliefRequisitionDetailService
    {
        private readonly IUnitOfWork _unitOfWork;


        public ReliefRequisitionDetailService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddReliefRequisitionDetail(ReliefRequisitionDetail reliefRequisitionDetail)
        {
            _unitOfWork.ReliefRequisitionDetailRepository.Add(reliefRequisitionDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool EditReliefRequisitionDetail(ReliefRequisitionDetail reliefRequisitionDetail)
        {
            _unitOfWork.ReliefRequisitionDetailRepository.Edit(reliefRequisitionDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteReliefRequisitionDetail(ReliefRequisitionDetail reliefRequisitionDetail)
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
        public List<ReliefRequisitionDetail> GetAllReliefRequisitionDetail()
        {
            return _unitOfWork.ReliefRequisitionDetailRepository.GetAll();
        }
        public ReliefRequisitionDetail FindById(int id)
        {
            return _unitOfWork.ReliefRequisitionDetailRepository.FindById(id);
        }
        public List<ReliefRequisitionDetail> FindBy(Expression<Func<ReliefRequisitionDetail, bool>> predicate)
        {
            return _unitOfWork.ReliefRequisitionDetailRepository.FindBy(predicate);
        }
        public IEnumerable<ReliefRequisitionDetail> Get(
          Expression<Func<ReliefRequisitionDetail, bool>> filter = null,
          Func<IQueryable<ReliefRequisitionDetail>, IOrderedQueryable<ReliefRequisitionDetail>> orderBy = null,
          string includeProperties = "")
        {
            return _unitOfWork.ReliefRequisitionDetailRepository.Get(filter, orderBy, includeProperties);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


