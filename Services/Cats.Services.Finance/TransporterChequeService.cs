using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;


namespace Cats.Services.Finance
{

    public class TransporterChequeService : ITransporterChequeService
    {
        private readonly IUnitOfWork _unitOfWork;


        public TransporterChequeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddTransporterCheque(TransporterCheque transporterCheque)
        {
            _unitOfWork.TransporterChequeRepository.Add(transporterCheque);
            _unitOfWork.Save();
            return true;

        }
        public bool EditTransporterCheque(TransporterCheque transporterCheque)
        {
            _unitOfWork.TransporterChequeRepository.Edit(transporterCheque);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteTransporterCheque(TransporterCheque transporterCheque)
        {
            if (transporterCheque == null) return false;
            _unitOfWork.TransporterChequeRepository.Delete(transporterCheque);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.TransporterChequeRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.TransporterChequeRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<TransporterCheque> GetAllTransporterCheque()
        {
            return _unitOfWork.TransporterChequeRepository.GetAll();
        }
        public TransporterCheque FindById(int id)
        {
            return _unitOfWork.TransporterChequeRepository.FindById(id);
        }
        public List<TransporterCheque> FindBy(Expression<Func<TransporterCheque, bool>> predicate)
        {
            return _unitOfWork.TransporterChequeRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}

 
      
