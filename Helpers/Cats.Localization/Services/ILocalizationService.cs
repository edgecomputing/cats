using System;
using Cats.Localization.Models;

namespace Cats.Localization.Services
{
    public interface ILocalizationService : IDisposable
    {
        bool AddLanguage(Language language);
        bool UpdateLanguage(Language language);
        bool DeleteLanguage(Language language, bool cascadeDelete = true);

        bool AddPhrase(Phrase phrase);
        bool UpdatePhrase(Phrase phrase);
        bool DeletePhrase(Phrase phrase);

        bool AddPage(Page page);
        bool UpdatePage(Page page);
        bool DeletePage(Page page);
    }

    
}
