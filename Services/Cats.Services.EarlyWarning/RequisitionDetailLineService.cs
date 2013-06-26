

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;


namespace Cats.Services.EarlyWarning
{

    public class RequisitionDetailLineService : IRequisitionDetailLineService
    {
        private readonly IUnitOfWork _unitOfWork;


        public RequisitionDetailLineService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddRequisitionDetailLine(RequisitionDetailLine requisitionDetailLine)
        {
            _unitOfWork.RequisitionDetailLineRepository.Add(requisitionDetailLine);
            _unitOfWork.Save();
            return true;

        }
        public bool EditRequisitionDetailLine(RequisitionDetailLine requisitionDetailLine)
        {
            _unitOfWork.RequisitionDetailLineRepository.Edit(requisitionDetailLine);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteRequisitionDetailLine(RequisitionDetailLine requisitionDetailLine)
        {
            if (requisitionDetailLine == null) return false;
            _unitOfWork.RequisitionDetailLineRepository.Delete(requisitionDetailLine);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.RequisitionDetailLineRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.RequisitionDetailLineRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<RequisitionDetailLine> GetAllRequisitionDetailLine()
        {
            return _unitOfWork.RequisitionDetailLineRepository.GetAll();
        }
        public RequisitionDetailLine FindById(int id)
        {
            return _unitOfWork.RequisitionDetailLineRepository.FindById(id);
        }
        public List<RequisitionDetailLine> FindBy(Expression<Func<RequisitionDetailLine, bool>> predicate)
        {
            return _unitOfWork.RequisitionDetailLineRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


