

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hub;


namespace Cats.Services.Hub
{

    public class LedgerService : ILedgerService
    {
        private readonly IUnitOfWork _unitOfWork;


        public LedgerService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #region Default Service Implementation
        public bool AddLedger(Ledger entity)
        {
            _unitOfWork.LedgerRepository.Add(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool EditLedger(Ledger entity)
        {
            _unitOfWork.LedgerRepository.Edit(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteLedger(Ledger entity)
        {
            if (entity == null) return false;
            _unitOfWork.LedgerRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.LedgerRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.LedgerRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Ledger> GetAllLedger()
        {
            return _unitOfWork.LedgerRepository.GetAll();
        }
        public Ledger FindById(int id)
        {
            return _unitOfWork.LedgerRepository.FindById(id);
        }
        public List<Ledger> FindBy(Expression<Func<Ledger, bool>> predicate)
        {
            return _unitOfWork.LedgerRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}

 
      
