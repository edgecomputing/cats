

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hub;

namespace Cats.Services.Hub
{

    public class SessionHistoryService : ISessionHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;


        public SessionHistoryService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddSessionHistory(SessionHistory entity)
        {
            _unitOfWork.SessionHistoryRepository.Add(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool EditSessionHistory(SessionHistory entity)
        {
            _unitOfWork.SessionHistoryRepository.Edit(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteSessionHistory(SessionHistory entity)
        {
            if (entity == null) return false;
            _unitOfWork.SessionHistoryRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.SessionHistoryRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.SessionHistoryRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<SessionHistory> GetAllSessionHistory()
        {
            return _unitOfWork.SessionHistoryRepository.GetAll();
        }
        public SessionHistory FindById(int id)
        {
            return _unitOfWork.SessionHistoryRepository.FindById(id);
        }
        public List<SessionHistory> FindBy(Expression<Func<SessionHistory, bool>> predicate)
        {
            return _unitOfWork.SessionHistoryRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


