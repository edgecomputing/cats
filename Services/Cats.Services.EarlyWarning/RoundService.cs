

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;


namespace DRMFSS.BLL.Services
{

    public class RoundService : IRoundService
    {
        private readonly IUnitOfWork _unitOfWork;


        public RoundService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddRound(Round round)
        {
            _unitOfWork.RoundRepository.Add(round);
            _unitOfWork.Save();
            return true;

        }
        public bool EditRound(Round round)
        {
            _unitOfWork.RoundRepository.Edit(round);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteRound(Round round)
        {
            if (round == null) return false;
            _unitOfWork.RoundRepository.Delete(round);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.RoundRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.RoundRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Round> GetAllRound()
        {
            return _unitOfWork.RoundRepository.GetAll();
        }
        public Round FindById(int id)
        {
            return _unitOfWork.RoundRepository.FindById(id);
        }
        public List<Round> FindBy(Expression<Func<Round, bool>> predicate)
        {
            return _unitOfWork.RoundRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


