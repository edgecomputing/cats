

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hub;


namespace Cats.Services.Hub
{

    public class SessionAttemptService : ISessionAttemptService
    {
        private readonly IUnitOfWork _unitOfWork;


        public SessionAttemptService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddSessionAttempt(SessionAttempt entity)
        {
            _unitOfWork.SessionAttemptRepository.Add(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool EditSessionAttempt(SessionAttempt entity)
        {
            _unitOfWork.SessionAttemptRepository.Edit(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteSessionAttempt(SessionAttempt entity)
        {
            if (entity == null) return false;
            _unitOfWork.SessionAttemptRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.SessionAttemptRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.SessionAttemptRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<SessionAttempt> GetAllSessionAttempt()
        {
            return _unitOfWork.SessionAttemptRepository.GetAll();
        }
        public SessionAttempt FindById(int id)
        {
            return _unitOfWork.SessionAttemptRepository.FindById(id);
        }
        public List<SessionAttempt> FindBy(Expression<Func<SessionAttempt, bool>> predicate)
        {
            return _unitOfWork.SessionAttemptRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}

 
      
