using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
        private IGenericRepository<UserInfo> userInfoRepo;
        private IGenericRepository<UserProfile> userProfileRepo;
        private IGenericRepository<ForgetPasswordRequest> forgetPasswordRequestRepo;
        private IGenericRepository<Setting> settingRepo;

        public IGenericRepository<UserInfo> UserInfoRepository
        {
            get { return userInfoRepo ?? (this.userInfoRepo = new GenericRepository<UserInfo>(_context)); }
        }

        public IGenericRepository<UserProfile> UserProfileRepository
        {
            get { return userProfileRepo ?? (this.userProfileRepo = new GenericRepository<UserProfile>(_context)); }
        }

        public IGenericRepository<ForgetPasswordRequest> ForgetPasswordRequestRepository
        {
            get { return forgetPasswordRequestRepo ?? (this.forgetPasswordRequestRepo = new GenericRepository<ForgetPasswordRequest>(_context)); }
        }
        public IGenericRepository<Setting> SettingRepository
        {
            get { return settingRepo ?? (this.settingRepo = new GenericRepository<Setting>(_context)); }
        }

        #endregion

        #region UnitOfWork CRUD method(s)

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException exception)
            {


            }

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
