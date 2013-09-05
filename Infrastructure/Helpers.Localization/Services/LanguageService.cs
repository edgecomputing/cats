using System;
using System.Collections.Generic;
using System.Linq;
using LanguageHelpers.Localization.Data;
using LanguageHelpers.Localization.Models;

namespace LanguageHelpers.Localization.Services
{
   public class LanguageService:ILanguageService
    {

       private readonly  IUnitOfWork _unitOfWork;
      

       public LanguageService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }
        public bool AddLanguage(Language language)
        {
            _unitOfWork.LanguageRepositroy.Add(language);
            var texts = _unitOfWork.LocalizedTextRepository.GetAll();
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
            _unitOfWork.LanguageRepositroy.Delete(language);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity=_unitOfWork.LanguageRepositroy.FindById(id);
            if (entity == null) return false;
            _unitOfWork.LanguageRepositroy.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditLanguage(Language language)
        {
            _unitOfWork.LanguageRepositroy.Edit(language);
            _unitOfWork.Save();
            return true;
        }

        public Language FindById(int id)
        {
            return _unitOfWork.LanguageRepositroy.FindById(id);
        }

        public List<Language> GetAllLanguage()
        {
            return _unitOfWork.LanguageRepositroy.GetAll();
        }

        public List<Language> FindBy(System.Linq.Expressions.Expression<Func<Language, bool>> predicate)
        {
            return _unitOfWork.LanguageRepositroy.FindBy(predicate);
        }

        public IEnumerable<Language> Get(System.Linq.Expressions.Expression<Func<Language, bool>> filter = null, Func<IQueryable<Language>, IOrderedQueryable<Language>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.LanguageRepositroy.Get(filter, orderBy, includeProperties);
        }

        public bool Save()
        {
            _unitOfWork.Save();
            return true;
        }
    }
}
