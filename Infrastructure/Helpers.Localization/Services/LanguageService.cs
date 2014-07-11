using System;
using System.Collections.Generic;
using System.Linq;
using LanguageHelpers.Localization.Data;
using LanguageHelpers.Localization.Models;

namespace LanguageHelpers.Localization.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LanguageService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public bool AddLanguage(Language language)
        {
            _unitOfWork.LanguageRepository.Add(language);
            var texts = _unitOfWork.LocalizedTextRepository.FindBy(m=>m.LanguageCode=="EN");
            foreach (var localizedText in texts)
            {
                var newText = new LocalizedText()
                    {
                        LanguageCode = language.LanguageCode,
                        TextKey = localizedText.TextKey
                    };
                _unitOfWork.LocalizedTextRepository.Add(newText);
            }
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteLanguage(Language language)
        {
            if (language == null) return false;
            _unitOfWork.LanguageRepository.Delete(language);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.LanguageRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.LanguageRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditLanguage(Language language)
        {
            _unitOfWork.LanguageRepository.Edit(language);
            _unitOfWork.Save();
            return true;
        }

        public Language FindById(int id)
        {
            return _unitOfWork.LanguageRepository.FindById(id);
        }

        public List<Language> GetAllLanguage()
        {
            return _unitOfWork.LanguageRepository.GetAll();
        }

        public List<Language> FindBy(System.Linq.Expressions.Expression<Func<Language, bool>> predicate)
        {
            return _unitOfWork.LanguageRepository.FindBy(predicate);
        }

        public IEnumerable<Language> Get(System.Linq.Expressions.Expression<Func<Language, bool>> filter = null, Func<IQueryable<Language>, IOrderedQueryable<Language>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.LanguageRepository.Get(filter, orderBy, includeProperties);
        }

        public bool Save()
        {
            _unitOfWork.Save();
            return true;
        }
    }
}
