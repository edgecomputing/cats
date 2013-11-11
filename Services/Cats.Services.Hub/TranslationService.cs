using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Data.Hub;
using Cats.Models.Hubs;

namespace Cats.Services.Hub
{
    public class TranslationService:ITranslationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TranslationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string GetForText(string text, string langauge)
        {
            var Trans = _unitOfWork.TranslationRepository.FindBy(t => t.Phrase.Trim() == text.Trim() && t.LanguageCode == langauge).FirstOrDefault();
           
            var Translation1 = Trans !=null ? Trans.TranslatedText : null;

            if (Translation1 == null)
            {
                Translation translation = new Translation();
                translation.LanguageCode = langauge;
                translation.Phrase = text.Trim();
                translation.TranslatedText = text.Trim();
                _unitOfWork.TranslationRepository.Add(translation);
                _unitOfWork.Save();

                Translation english = null;
                if (langauge != "en")
                {
                    english = _unitOfWork.TranslationRepository.FindBy(t => t.Phrase == text && t.LanguageCode == "en").FirstOrDefault();
                }
                if (english == null)
                {
                    translation = new Translation();
                    translation.LanguageCode = "en";
                    translation.Phrase = translation.TranslatedText = text.Trim();
                    _unitOfWork.TranslationRepository.Add(translation);
                    _unitOfWork.Save();
                }
                else
                {
                    return english.TranslatedText;
                }
                return text;
            }
            return Translation1;
        }

        public List<Translation> GetAll(string languageCode)
        {
            return _unitOfWork.TranslationRepository.FindBy(t => t.LanguageCode == languageCode).OrderBy(o => o.Phrase).ToList();
        }

        #region Default Service Implementation
        public bool AddTranslation(Translation entity)
        {
            _unitOfWork.TranslationRepository.Add(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool EditTranslation(Translation entity)
        {
            _unitOfWork.TranslationRepository.Edit(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteTranslation(Translation entity)
        {
            if (entity == null) return false;
            _unitOfWork.TranslationRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.TranslationRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.TranslationRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Translation> GetAllTranslation()
        {
            return _unitOfWork.TranslationRepository.GetAll();
        }
        public Translation FindById(int id)
        {
            return _unitOfWork.TranslationRepository.FindById(id);
        }
        public List<Translation> FindBy(Expression<Func<Translation, bool>> predicate)
        {
            return _unitOfWork.TranslationRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }
    }
}




 
      
