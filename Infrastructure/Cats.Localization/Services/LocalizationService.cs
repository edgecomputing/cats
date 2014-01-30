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
            _unitOfWork.LanguageRepository.Add(language);

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

        public bool AddLanguage(Language language, bool populateDefaultValues)
        {
            if (this.AddLanguage(language))
            {
                try
                {
                    // Get a list of default language texts
                    var defaultTexts = _unitOfWork.LocalizedTextRepository.FindBy(t => t.LanguageCode == "EN").ToList();

                    // Add each phrases to the Localized Phrases repo with the current language as the localized text
                    defaultTexts.ForEach(text => _unitOfWork.LocalizedTextRepository.Add(
                        new LocalizedText
                            {
                                LanguageCode = language.LanguageCode,
                                PageId = text.PageId,
                                TextKey = text.TextKey,
                                TranslatedText = text.TranslatedText
                            }));
                    // Commit changes to the database
                    _unitOfWork.Save();

                    // If I get this far then it is my luckey day
                    return true;
                }
                catch (Exception exception)
                {
                    // TODO: Log exception
                    throw new ApplicationException(string.Format("Error creating new language {0}", language), exception);
                }
            }

            return false;
        }

        public bool UpdateLanguage(Language language)
        {
            if (null == language) return false;
            _unitOfWork.LanguageRepository.Edit(language);
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
            // INFO: Make sure that the relationship in the database is set to cascade delete localized phrases for the current language
            try
            {
                _unitOfWork.Save();
                return true;
            }
            catch (Exception exception)
            {
                // Log error here.
                throw new ApplicationException(string.Format("Error occured deleting the language: {0}", language.LanguageName), exception);
            }
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
            _unitOfWork.PageRepository.Edit(page);
            try
            {
                _unitOfWork.Save();
                return true;
            }
            catch (Exception exception)
            {
                // TODO: Log error 
                throw new ApplicationException(string.Format("Error updating page information"), exception);
            }
        }

        public bool DeletePage(Page page)
        {
            // INFO: Remember to set cascade delete for Page and LocalizedText
            try
            {
                _unitOfWork.PageRepository.Delete(page);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception exception)
            {
                // TODO: Log error here
                throw new ApplicationException("Error removing page information", exception);
            }
        }

        #endregion

        #region Translation Methods
        public List<LocalizedText> GetLocalizedTextForPage(string pageName, string language = "EN")
        {
            try
            {
                var page = _unitOfWork.PageRepository.Get(p => p.PageKey == pageName).Single();
                return GetLocalizedTextForPage(page, language);
            }
            catch (Exception)
            {
                // TODO: Log error
                throw new ApplicationException(string.Format("Error fetching localized text for page: {0}", pageName));
            }
        }

        public List<LocalizedText> GetLocalizedTextForPage(Page page, string language = "EN")
        {
            try
            {
                var phrases = LocalizedTextForPage(page.PageId, language);
                return phrases.ToList();
            }
            catch (Exception)
            {
                // TODO: Log error
                throw new ApplicationException(string.Format("Error fetching localized text for page: {0}", page.PageKey));
            }
        }

        public Dictionary<string, string> GetLocalizedTextDictionaryForPage(string pageName, string language)
        {
            try
            {
                var page = _unitOfWork.PageRepository.Get(p => p.PageKey == pageName).Single();
                var phrases = LocalizedTextForPage(page.PageId, language);
                var result = new Dictionary<string, string>();
                phrases.ToList().ForEach(text => result.Add(text.TextKey, text.TranslatedText));
                return result;
            }
            catch (Exception ex)
            {
                // TODO: Log error
                throw new ApplicationException(string.Format("Error fetching localized text dictionary for page: {0}", pageName),ex);
            }
        }

        /// <summary>
        /// Adds new entries into the localized phrases table for all phrases in the 'translation' dictionary.
        /// It also associates the page 'page' with the list of translated phrases
        /// </summary>
        /// <param name="page">Name of the page to translate</param>
        /// <param name="translations">Dictionary of translation terms [Phrase][TranslatedPhrase]</param>
        /// <param name="language">Language to translate to</param>
        /// <returns>Boolean flag for success/failure</returns>
        public bool TranslatePage(string page, Dictionary<string, string> translations, string language = "EN")
        {
            try
            {
                // TODO: Consider filtering existing translations before adding everything that came
                //       as parameter 'translations'.
                // WARNING: The current implemenation only considers new entries and categorically ignores
                //          updates.

                var pageToTranslate = _unitOfWork.PageRepository.Get(p => p.PageKey == page).Single();
                TranslatePage(pageToTranslate, translations, language);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception exception)
            {
                // TODO: Log error
                throw new ApplicationException(string.Format("Error translating page: {0}.", page), exception);
            }
        }
        /// <summary>
        /// Adds new entries into the localized phrases table for all phrases in the 'translation' dictionary.
        /// It also associates the page 'page' with the list of translated phrases
        /// </summary>
        /// <param name="page">Page object representing the page to translate</param>
        /// <param name="translations">Dictionary of translation terms [Phrase][TranslatedPhrase]</param>
        /// <param name="language">Language to translate to</param>
        private void TranslatePage(Page page, Dictionary<string, string> translations, string language = "EN")
        {
            // For all phrases inside 'translation', add a new translation entry by associating it
            // with the specified language.
            page.LocalizedTexts.ToList().ForEach(t =>
            {
                t.TranslatedText = TranslatedTextFromDictionaryOrDefault(t.TextKey, translations);
                t.LanguageCode = language;
            });
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks if a corresponding translated entry exist inside translations dictionary. If no match
        /// if found then it will return the language neutral phrase (the one passed as parameter).
        /// </summary>
        /// <param name="phrase">Phrase to match within the translation dictionary as key</param>
        /// <param name="translations">Dictionary of translated terms.</param>
        /// <returns>Corresponding translated item fro the dictionary or language neutral string</returns>
        private string TranslatedTextFromDictionaryOrDefault(string phrase, Dictionary<string, string> translations)
        {
            if (translations.ContainsKey(phrase))
            {
                return translations[phrase];
            }

            return phrase;
        }

        private IEnumerable<LocalizedText> LocalizedTextForPage(int pageId, string language)
        {
            return _unitOfWork.LocalizedTextRepository.Get(t => t.PageId == pageId && t.LanguageCode == language);
        }

        #endregion

    }
}
