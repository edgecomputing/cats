using System;
using LanguageHelpers.Localization.Data.Repository;
using LanguageHelpers.Localization.Models;

namespace LanguageHelpers.Localization.Data
{
    /// <summary>
    /// UnitOfwork implementation for security module    
    /// </summary>
    /// 
    public class UnitOfWork : IUnitOfWork
    {
        #region Constructors and private vars

        private readonly LocalizationContext _context;
        private IGenericRepository<LocalizedText> localizedTextRepository;
        private IGenericRepository<Language> languageRepository; 
        //private readonly SecurityContext _context;
              
        public UnitOfWork()
        {
            this._context = new LocalizationContext();
        }
        #endregion
        
        public IGenericRepository<LocalizedText> LocalizedTextRepository
        {
            get { return this.localizedTextRepository ?? (this.localizedTextRepository = new GenericRepository<LocalizedText>(_context)); }
        }

        public IGenericRepository<Language> LanguageRepository
        {
            get { return this.LanguageRepository ?? (this.languageRepository = new GenericRepository<Language>(_context)); }
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
            //TODO:Commented Out becasue of Error
            _context.SaveChanges();
        }
    }
}