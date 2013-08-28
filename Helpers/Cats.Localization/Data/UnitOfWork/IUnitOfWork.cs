using System;
using Cats.Localization.Data.Repository;
using Cats.Localization.Models;

namespace Cats.Localization.Data.UnitOfWork
{
    /// <summary>
    /// UnitOfWork class to manage persistence and retrival of modles associated with
    /// the security module
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<LocalizedPhrase> LocalizationPhraseRepository { get; }        
        IGenericRepository<Language> LanguageRepositroy { get; }
        IGenericRepository<Phrase> PhraseRepository { get; }
        IGenericRepository<Page> PageRepository { get; }

        void Save();
    }
}
