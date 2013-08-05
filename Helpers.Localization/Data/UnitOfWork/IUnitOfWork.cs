using System;
using System.Collections.Generic;
using Helpers.Localization.Models;
using Helpers.Localization.Data.Repository;
namespace Helpers.Localization.Data.UnitWork
{
    /// <summary>
    /// UnitOfWork class to manage persistence and retrival of modles associated with
    /// the security module
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
         IGenericRepository<LocalizedText> LocalizedTextRepository { get; }

        void Save();
    }
}
