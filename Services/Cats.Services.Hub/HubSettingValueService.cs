

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hubs;


namespace Cats.Services.Hub
{

    public class HubSettingValueService : IHubSettingValueService
    {
        private readonly IUnitOfWork _unitOfWork;


        public HubSettingValueService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddHubSettingValue(HubSettingValue hubSettingValue)
        {
            _unitOfWork.HubSettingValueRepository.Add(hubSettingValue);
            _unitOfWork.Save();
            return true;

        }
        public bool EditHubSettingValue(HubSettingValue hubSettingValue)
        {
            _unitOfWork.HubSettingValueRepository.Edit(hubSettingValue);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteHubSettingValue(HubSettingValue hubSettingValue)
        {
            if (hubSettingValue == null) return false;
            _unitOfWork.HubSettingValueRepository.Delete(hubSettingValue);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.HubSettingValueRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.HubSettingValueRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<HubSettingValue> GetAllHubSettingValue()
        {
            return _unitOfWork.HubSettingValueRepository.GetAll();
        }
        public HubSettingValue FindById(int id)
        {
            return _unitOfWork.HubSettingValueRepository.FindById(id);
        }
        public List<HubSettingValue> FindBy(Expression<Func<HubSettingValue, bool>> predicate)
        {
            return _unitOfWork.HubSettingValueRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


