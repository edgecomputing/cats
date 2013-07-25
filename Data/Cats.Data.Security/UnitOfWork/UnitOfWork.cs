using System;
using System.Collections.Generic;
using System.Linq;
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
        private IGenericRepository<UserProfile> userProfileRepo;
        private IGenericRepository<UserPreference> userPreferenceRepo;

        public IGenericRepository<UserAccount> UserRepository
        {
            get { return this.userRepo ?? (this.userRepo = new GenericRepository<UserAccount>(_context)); }

        }

        public IGenericRepository<UserInfo> UserInfoRepository
        {
            get { return userInfoRepo ?? (this.userInfoRepo = new GenericRepository<UserInfo>(_context)); }
        }

        public IGenericRepository<UserProfile> UserProfileRepository
        {
            get { return userProfileRepo ?? (this.userProfileRepo = new GenericRepository<UserProfile>(_context)); }
        }

        public IGenericRepository<UserPreference> UserPreferenceRepository
        {
            get { return userPreferenceRepo ?? (this.userPreferenceRepo = new GenericRepository<UserPreference>(_context)); }
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
