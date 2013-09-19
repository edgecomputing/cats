using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Common
{
    public class UserDashboardPreferenceService : IUserDashboardPreferenceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserDashboardPreferenceService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        #endregion

        #region Implementation of IUserDashboardPreferenceService

        public bool AddUserDashboardPreference(UserDashboardPreference userDashboardPreference)
        {
            _unitOfWork.UserDashboardPreferenceRepository.Add(userDashboardPreference);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteUserDashboardPreference(UserDashboardPreference userDashboardPreference)
        {
            if (userDashboardPreference == null) return false;
            _unitOfWork.UserDashboardPreferenceRepository.Delete(userDashboardPreference);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.UserDashboardPreferenceRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.UserDashboardPreferenceRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditUserDashboardPreference(UserDashboardPreference userDashboardPreference)
        {
            _unitOfWork.UserDashboardPreferenceRepository.Edit(userDashboardPreference);
            _unitOfWork.Save();
            return true;
        }

        public UserDashboardPreference FindById(int id)
        {
            return _unitOfWork.UserDashboardPreferenceRepository.FindById(id);
        }

        public List<UserDashboardPreference> GetAllUserDashboardPreference()
        {
            return _unitOfWork.UserDashboardPreferenceRepository.GetAll();
        }

        public List<UserDashboardPreference> FindBy(Expression<Func<UserDashboardPreference, bool>> predicate)
        {
            return _unitOfWork.UserDashboardPreferenceRepository.FindBy(predicate);
        }

        public IEnumerable<UserDashboardPreference> Get(Expression<Func<UserDashboardPreference, bool>> filter = null, Func<IQueryable<UserDashboardPreference>, IOrderedQueryable<UserDashboardPreference>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.UserDashboardPreferenceRepository.Get(filter, orderBy, includeProperties);
        }

        #endregion
    }
}
