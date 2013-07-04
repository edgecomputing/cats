using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.Security;
using Cats.Models.Security;

namespace Cats.Services.Security
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserAccountService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #region Default Service Implementation
        public bool Add(User entity)
        {
            _unitOfWork.UserRepository.Add(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool Save(User entity)
        {
            _unitOfWork.UserRepository.Edit(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool Delete(User entity)
        {
            if (entity == null) return false;
            _unitOfWork.UserRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.UserRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.UserRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public List<User> GetAll()
        {
            return _unitOfWork.UserRepository.GetAll();
        }

        public User FindById(int id)
        {
            return _unitOfWork.UserRepository.FindById(id);
        }

        public List<User> FindBy(Expression<Func<User, bool>> predicate)
        {
            return _unitOfWork.UserRepository.FindBy(predicate);
        }

        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


