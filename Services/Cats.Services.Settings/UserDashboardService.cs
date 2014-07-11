using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Settings
{

    public class UserDashboardService : IUserDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;


        public UserDashboardService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddUserDashboard(UserDashboard userDashboard)
        {
            _unitOfWork.UserDashboardRepository.Add(userDashboard);
            _unitOfWork.Save();
            return true;

        }
        public bool EditUserDashboard(UserDashboard userDashboard)
        {
            _unitOfWork.UserDashboardRepository.Edit(userDashboard);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteUserDashboard(UserDashboard userDashboard)
        {
            if (userDashboard == null) return false;
            _unitOfWork.UserDashboardRepository.Delete(userDashboard);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.UserDashboardRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.UserDashboardRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<UserDashboard> GetAllUserDashboard()
        {
            return _unitOfWork.UserDashboardRepository.GetAll();
        }
        public UserDashboard FindById(int id)
        {
            return _unitOfWork.UserDashboardRepository.FindById(id);
        }
        public List<UserDashboard> FindBy(Expression<Func<UserDashboard, bool>> predicate)
        {
            return _unitOfWork.UserDashboardRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


