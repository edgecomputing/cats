

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hub;


namespace Cats.Services.Hub
{

    public class LedgerTypeService : ILedgerTypeService
    {
        private readonly IUnitOfWork _unitOfWork;


        public LedgerTypeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddLedgerType(LedgerType entity)
        {
            _unitOfWork.LedgerTypeRepository.Add(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool EditLedgerType(LedgerType entity)
        {
            _unitOfWork.LedgerTypeRepository.Edit(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteLedgerType(LedgerType entity)
        {
            if (entity == null) return false;
            _unitOfWork.LedgerTypeRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.LedgerTypeRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.LedgerTypeRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<LedgerType> GetAllLedgerType()
        {
            return _unitOfWork.LedgerTypeRepository.GetAll();
        }
        public LedgerType FindById(int id)
        {
            return _unitOfWork.LedgerTypeRepository.FindById(id);
        }
        public List<LedgerType> FindBy(Expression<Func<LedgerType, bool>> predicate)
        {
            return _unitOfWork.LedgerTypeRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


