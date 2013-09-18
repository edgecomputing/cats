

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hub;


namespace Cats.Services.Hub
{

    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;


        public RoleService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddRole(Role entity)
        {
            _unitOfWork.RoleRepository.Add(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool EditRole(Role entity)
        {
            _unitOfWork.RoleRepository.Edit(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteRole(Role entity)
        {
            if (entity == null) return false;
            _unitOfWork.RoleRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.RoleRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.RoleRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Role> GetAllRole()
        {
            return _unitOfWork.RoleRepository.GetAll();
        }
        public Role FindById(int id)
        {
            return _unitOfWork.RoleRepository.FindById(id);
        }
        public List<Role> FindBy(Expression<Func<Role, bool>> predicate)
        {
            return _unitOfWork.RoleRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}

 
      
