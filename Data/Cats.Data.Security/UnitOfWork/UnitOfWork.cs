using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using Cats.Models.Security;
using System.Data.Entity;
using log4net;
using System.Text;


namespace Cats.Data.Security
{
    /// <summary>
    /// UnitOfwork implementation for security module    
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Ctors and private vars

        private readonly SecurityContext _context;
        private readonly ILog _log;

        public UnitOfWork(ILog log)
        {
            this._context = new SecurityContext();
            _log = log;
        }

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
                //var log = new Logger();
                //log.LogAllErrorsMesseges(exception, _log);

                //for (var eCurrent = exception; eCurrent != null; eCurrent = (DbEntityValidationException)eCurrent.InnerException)
                //{
                //    StringBuilder errorMsg = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",,);
                //    _log.Error(eCurrent.Message, eCurrent.GetBaseException());
                //}

                for (var eCurrent = exception; eCurrent != null; eCurrent = (DbEntityValidationException)eCurrent.InnerException)
                {
                    foreach (var eve in eCurrent.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);

                        StringBuilder errorMsg = new StringBuilder(String.Empty);
                        var s = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        errorMsg.Append(s);

                        foreach (var ve in eve.ValidationErrors)
                        {
                            errorMsg.Append(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));

                            _log.Error(errorMsg, eCurrent.GetBaseException());
                        }
                    } 
                }

               // throw;

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
