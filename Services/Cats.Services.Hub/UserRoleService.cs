

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hubs;


namespace Cats.Services.Hub
{

    public class UserRoleService : IUserRoleService
    {
        private readonly IUnitOfWork _unitOfWork;


        public UserRoleService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddUserRole(UserRole entity)
        {
            _unitOfWork.UserRoleRepository.Add(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool EditUserRole(UserRole entity)
        {
            _unitOfWork.UserRoleRepository.Edit(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteUserRole(UserRole entity)
        {
            if (entity == null) return false;
            _unitOfWork.UserRoleRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.UserRoleRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.UserRoleRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<UserRole> GetAllUserRole()
        {
            return _unitOfWork.UserRoleRepository.GetAll();
        }
        public UserRole FindById(int id)
        {
            return _unitOfWork.UserRoleRepository.FindById(id);
        }
        public List<UserRole> FindBy(Expression<Func<UserRole, bool>> predicate)
        {
            return _unitOfWork.UserRoleRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

        public List<UserRole> Get(Expression<Func<UserRole, bool>> filter = null, Func<System.Linq.IQueryable<UserRole>, System.Linq.IOrderedQueryable<UserRole>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.UserRoleRepository.Get(filter, orderBy, includeProperties).ToList();
        }
    }
}


