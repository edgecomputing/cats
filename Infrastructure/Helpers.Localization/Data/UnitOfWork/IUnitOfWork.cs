using System;
using LanguageHelpers.Localization.Models;
using LanguageHelpers.Localization.Data.Repository;

namespace LanguageHelpers.Localization.Data
{
    /// <summary>
    /// UnitOfWork class to manage persistence and retrival of modles associated with
    /// the security module
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
         IGenericRepository<LocalizedText> LocalizedTextRepository { get; }
        IGenericRepository<Language> LanguageRepositroy { get; } 

        void Save();
    }
}
