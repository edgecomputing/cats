using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Localization.Data.UnitOfWork;
using Cats.Localization.Models;

namespace Cats.Localization.Services
{
    public class LocalizationService : ILocalizationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LocalizationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        #region Language CRUD

        public bool AddLanguage(Language language)
        {
            _unitOfWork.LanguageRepositroy.Add(language);

            try
            {
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateLanguage(Language language)
        {
            if (null == language) return false;
            _unitOfWork.LanguageRepositroy.Edit(language);
            try
            {
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteLanguage(Language language, bool cascadeDelete = true)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Page CRUD

        public bool AddPage(Page page)
        {
            _unitOfWork.PageRepository.Add(page);

            try
            {
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdatePage(Page page)
        {
            throw new NotImplementedException();
        }

        public bool DeletePage(Page page)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Phrase CRUD

        public bool AddPhrase(Phrase phrase)
        {
            _unitOfWork.PhraseRepository.Add(phrase);

            try
            {
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdatePhrase(Phrase phrase)
        {
            throw new NotImplementedException();
        }

        public bool DeletePhrase(Phrase phrase)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
