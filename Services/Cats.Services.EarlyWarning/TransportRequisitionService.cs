

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;


namespace Cats.Services.EarlyWarning
{

    public class TransportRequisitionService : ITransportRequisitionService
    {
        private readonly IUnitOfWork _unitOfWork;


        public TransportRequisitionService(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddTransportRequisition(TransportRequisition transportRequisition)
        {
            _unitOfWork.TransportRequisitionRepository.Add(transportRequisition);
            _unitOfWork.Save();
            return true;

        }
        public bool EditTransportRequisition(TransportRequisition transportRequisition)
        {
            _unitOfWork.TransportRequisitionRepository.Edit(transportRequisition);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteTransportRequisition(TransportRequisition transportRequisition)
        {
            if (transportRequisition == null) return false;
            _unitOfWork.TransportRequisitionRepository.Delete(transportRequisition);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.TransportRequisitionRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.TransportRequisitionRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<TransportRequisition> GetAllTransportRequisition()
        {
            return _unitOfWork.TransportRequisitionRepository.GetAll();
        }
        public TransportRequisition FindById(int id)
        {
            return _unitOfWork.TransportRequisitionRepository.FindById(id);
        }
        public List<TransportRequisition> FindBy(Expression<Func<TransportRequisition, bool>> predicate)
        {
            return _unitOfWork.TransportRequisitionRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


