

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hub;


namespace Cats.Services.Hub
{

    public class SMSService : ISMSService
    {
        private readonly IUnitOfWork _unitOfWork;


        public SMSService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddSMS(SMS entity)
        {
            _unitOfWork.SMSRepository.Add(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool EditSMS(SMS entity)
        {
            _unitOfWork.SMSRepository.Edit(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteSMS(SMS entity)
        {
            if (entity == null) return false;
            _unitOfWork.SMSRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.SMSRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.SMSRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<SMS> GetAllSMS()
        {
            return _unitOfWork.SMSRepository.GetAll();
        }
        public SMS FindById(int id)
        {
            return _unitOfWork.SMSRepository.FindById(id);
        }
        public List<SMS> FindBy(Expression<Func<SMS, bool>> predicate)
        {
            return _unitOfWork.SMSRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


