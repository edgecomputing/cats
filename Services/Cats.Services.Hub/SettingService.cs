

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hubs;


namespace Cats.Services.Hub
{

    public class SettingService : ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;


        public SettingService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddSetting(Setting setting)
        {
            _unitOfWork.SettingRepository.Add(setting);
            _unitOfWork.Save();
            return true;

        }
        public bool EditSetting(Setting setting)
        {
            _unitOfWork.SettingRepository.Edit(setting);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteSetting(Setting setting)
        {
            if (setting == null) return false;
            _unitOfWork.SettingRepository.Delete(setting);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.SettingRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.SettingRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Setting> GetAllSetting()
        {
            return _unitOfWork.SettingRepository.GetAll();
        }
        public Setting FindById(int id)
        {
            return _unitOfWork.SettingRepository.FindById(id);
        }
        public List<Setting> FindBy(Expression<Func<Setting, bool>> predicate)
        {
            return _unitOfWork.SettingRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }
        /// <summary>
        /// Gets the setting value.
        /// </summary>
        /// <param name="Key">The key.</param>
        /// <returns></returns>
        public string GetSettingValue(string Key)
        {
            string settingValue =
                _unitOfWork.SettingRepository.FindBy(t => t.Key == Key).Select(t => t.Value).FirstOrDefault();
           
            return settingValue;
        }

    }
}


