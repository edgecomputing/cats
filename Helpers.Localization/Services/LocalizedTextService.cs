using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using LanguageHelpers.Localization.Data.UnitOfWork;
using LanguageHelpers.Localization.Models;


namespace LanguageHelpers.Localization.Services
{
    public class LocalizedTextService : ILocalizedTextService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LocalizedTextService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool AddLocalizedText(LocalizedText item)
        {
            _unitOfWork.LocalizedTextRepository.Add(item);
            _unitOfWork.Save();
            return true;
        }
        public bool UpdateLocalizedText(LocalizedText item)
        {
            if (item == null) return false;
            _unitOfWork.LocalizedTextRepository.Edit(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteLocalizedText(LocalizedText item)
        {
            if (item == null) return false;
            _unitOfWork.LocalizedTextRepository.Delete(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var item = _unitOfWork.LocalizedTextRepository.FindById(id);
            return DeleteLocalizedText(item);
        }
        public LocalizedText FindById(int id)
        {
            return _unitOfWork.LocalizedTextRepository.FindById(id);
        }
        public List<LocalizedText> GetAllLocalizedText()
        {
            return _unitOfWork.LocalizedTextRepository.GetAll();

        }
        public List<LocalizedText> FindBy(Expression<Func<LocalizedText, bool>> predicate)
        {
            return _unitOfWork.LocalizedTextRepository.FindBy(predicate);

        }
        public string Translate(string key, string languageCode)
        {
            List<LocalizedText> list = FindBy(f => f.TextKey == key && f.LanguageCode==languageCode).ToList();
            if (list.Count >= 1)
            {
                return list[0].Value;
            }
            return key + "-" + languageCode;
        }
    }
}