using System;
using Cats.Localization.Data.Repository;
using Cats.Localization.Models;

namespace Cats.Localization.Data.UnitOfWork
{
    /// <summary>
    /// UnitOfWork class to manage persistence and retrival of modles
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<LocalizedText> LocalizedTextRepository { get; }        
        IGenericRepository<Language> LanguageRepository { get; }
        IGenericRepository<Page> PageRepository { get; }

        void Save();
    }
}
