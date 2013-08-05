using System;
using System.Collections.Generic;
using System.Linq;
using Helpers.Localization.Models;

using Helpers.Localization.Data.Repository;
using System.Data.Entity;

namespace Helpers.Localization.Data.UnitWork
{
    /// <summary>
    /// UnitOfwork implementation for security module    
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Ctors and private vars

        private readonly LocalizationContext _context;
        private IGenericRepository<LocalizedText> localizedTextRepository;
        // private readonly SecurityContext _context;
              
        public UnitOfWork()
        {
            this._context = new LocalizationContext();
        }

        #endregion


        
        public IGenericRepository<LocalizedText> LocalizedTextRepository
        {
            get { return this.localizedTextRepository ?? (this.localizedTextRepository = new GenericRepository<LocalizedText>(_context)); }

        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
