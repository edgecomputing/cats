using System;
using System.Collections.Generic;
using Cats.Localization.Models;

namespace Cats.Localization.Services
{
    public interface ILocalizationService : IDisposable
    {
        bool AddLanguage(Language language);
        bool AddLanguage(Language language, bool populateDefaultValues);
        bool UpdateLanguage(Language language);
        bool DeleteLanguage(Language language, bool cascadeDelete = true);

        bool AddPhrase(Phrase phrase);
        bool UpdatePhrase(Phrase phrase);
        bool DeletePhrase(Phrase phrase);

        bool AddPage(Page page);
        bool UpdatePage(Page page);
        bool DeletePage(Page page);

        //TODO: Add methods to do the following tasks
        
        /* 1. Retrive a list of phrases for a given page
         * 2. Fetch localized text for a phrase given the phrase and language
         * 3. Batch update list of phrases for a given page
         * 4. When a new language is created copy available phrases insert them in the localized text table
         *    with the coresponding language.
         * 
         */

        List<LocalizedPagePhrase> GetPhrasesForPage(Page page, string language = "EN");
        List<LocalizedPagePhrase> GetPhrasesForPage(string pageName, string language = "EN");
        Dictionary<string, string> GetPhrasesDictionaryForPage(string pageName, string language);

        string GetLocalizedPhrase(string phrase, string language);

        bool TranslatePhrase(string phrase, string translation, string language = "EN");

        bool TranslatePage(Page page, Dictionary<string,string> translations, string language="EN");


    }

    
}
