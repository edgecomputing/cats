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
                    // Get all phrases
                    var phrases = _unitOfWork.PhraseRepository.GetAll().ToList();

                    // Add each phrases to the Localized Phrases repo with the current language as the localized text
                    phrases.ForEach(phrase => _unitOfWork.LocalizedPhraseRepository.Add(
                        new LocalizedPhrase
                            {
                                LanguageCode = language.LanguageCode,
                                PhraseId = phrase.PhraseId,
                                TranslatedText = phrase.PhraseText
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
            // INFO: Remember to set cascade delete for Page and PagePhrase
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
            _unitOfWork.PhraseRepository.Edit(phrase);
            try
            {
                _unitOfWork.Save();
                return true;
            }
            catch (Exception exception)
            {
                // TODO: Log error 
                throw new ApplicationException(string.Format("Error updating phrase information"), exception);
            }
        }

        public bool DeletePhrase(Phrase phrase)
        {
            // INFO: Set cascade delete for Phrase and LocalizedPhrase tables
            try
            {
                _unitOfWork.PhraseRepository.Delete(phrase);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception exception)
            {
                // TODO: Log error here
                throw new ApplicationException("Error removing phrase information", exception);
            }
        }

        #endregion

        #region Translation Methods

        public List<LocalizedPagePhrase> GetPhrasesForPage(Page page, string language = "EN")
        {
            return GetPhrasesForPage(page.PageKey, language);
        }

        public List<LocalizedPagePhrase> GetPhrasesForPage(string pageName, string language = "EN")
        {
            try
            {
                var phrases = LocalizedPhrasesForPage(pageName, language);
                return phrases.ToList();
            }
            catch (Exception)
            {
                // TODO: Log error
                throw new ApplicationException("Error fetching phrases for page");
            }
        }

        public Dictionary<string, string> GetPhrasesDictionaryForPage(string pageName, string language)
        {
            try
            {
                var phrases = LocalizedPhrasesForPage(pageName, language);
                var result = new Dictionary<string, string>();
                phrases.ToList().ForEach(text => result.Add(text.PhraseText, text.TranslatedText));
                return result;
            }
            catch (Exception)
            {
                // TODO: Log error
                throw new ApplicationException("Error fetching phrases for page");
            }
        }

        /// <summary>
        /// Fetches the associated translation for the specified 'phrase' and 'language'
        /// </summary>
        /// <param name="phrase">Identifier for the phrase</param>
        /// <param name="language">Target language for the translation</param>
        /// <returns>Corresponding translation for 'phrase' to the language 'language'</returns>
        public string GetLocalizedPhrase(string phrase, string language)
        {
            // If we are requested translation to the default language; i.e. 'EN' the return
            // the requested text immediately
            if (language == "EN") return phrase;
            try
            {
                // Try to get translation for 'phrase' and force the expression to return a single object.
                var phraseTranslation =
                    _unitOfWork.LocalizedPhraseRepository.Get(
                        p => p.Phrase.PhraseText == phrase && p.LanguageCode == language).SingleOrDefault();

                if (phraseTranslation != null) return phraseTranslation.TranslatedText;
            }
            catch (Exception exception)
            {
                // TODO: Log error
                throw new ApplicationException(string.Format("Unable to fetch translation for phrase {0} to language {1}.", phrase, language), exception);
            }

            return phrase;
        }

        /// <summary>
        /// Inserts a new translation record for the specified phrase
        /// </summary>
        /// <param name="phrase">Phrase to translate</param>
        /// <param name="translation">The corresponding translation for 'phrase'</param>
        /// <param name="language">The language to translate to</param>
        /// <returns>Boolean value flagging success/failure</returns>
        public bool TranslatePhrase(string phrase, string translation, string language = "EN")
        {
            // Get the phrase to translate and add the associated text
            var phraseToTranslate = _unitOfWork.PhraseRepository.Get(p => p.PhraseText == phrase).Single();
            phraseToTranslate.LocalizedPhrases.Add(new LocalizedPhrase
                                                       {
                                                           LanguageCode = language,
                                                           PhraseId = phraseToTranslate.PhraseId,
                                                           TranslatedText = "",
                                                       });
            try
            {
                _unitOfWork.Save();
                return true;
            }
            catch (Exception exception)
            {
                // TODO: Log error here
                throw new ApplicationException(string.Format("Error inserting a new translation for text: {0}", phrase), exception);
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
            page.Phrases.ToList().ForEach(p => p.LocalizedPhrases.Add(new LocalizedPhrase
                                                                        {
                                                                            LanguageCode = language,
                                                                            PhraseId = p.PhraseId,
                                                                            TranslatedText = TranslatedTextFromDictionaryOrDefault(p.PhraseText, translations)
                                                                        }));
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

        private IEnumerable<LocalizedPagePhrase> LocalizedPhrasesForPage(string pageName, string language)
        {
            return _unitOfWork.PagePhraseRepository.Get(p => p.PageKey == pageName && p.LanguageCode == language);
        }


        #endregion
    }
}
