using System;
using Cats.Models.Security;
using System.Data.Entity;

namespace Cats.Data.Security
{
    /// <summary>
    /// UnitOfwork implementation for security module    
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Ctors and private vars

        private readonly SecurityContext _context;
              
        public UnitOfWork()
        {
            this._context = new SecurityContext();
        }

        #endregion

        #region UnitOfWork public properties
        private IGenericRepository<UserAccount> userRepo;
        private IGenericRepository<UserInfo> userInfoRepo;

        public IGenericRepository<UserAccount> UserRepository
        {
            get { return this.userRepo ?? (this.userRepo = new GenericRepository<UserAccount>(_context)); }

        }

        public IGenericRepository<UserInfo> UserInfoRepository
        {
            get { return userInfoRepo ?? (this.userInfoRepo = new GenericRepository<UserInfo>(_context)); }
        }
    
        #endregion

        #region UnitOfWork CRUD method(s)

        public void Save()
        {
            _context.SaveChanges();
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

        #endregion

    }
}
