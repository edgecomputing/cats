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
        IGenericRepository<LocalizedPhrase> LocalizedPhraseRepository { get; }        
        IGenericRepository<Language> LanguageRepository { get; }
        IGenericRepository<Phrase> PhraseRepository { get; }
        IGenericRepository<Page> PageRepository { get; }
        IGenericRepository<LocalizedPagePhrase> PagePhraseRepository { get; } 

        void Save();
    }
}
