

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;


namespace DRMFSS.BLL.Services
{

    public class AdminUnitTypeService : IAdminUnitTypeService
    {
        private readonly IUnitOfWork _unitOfWork;


        public AdminUnitTypeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddAdminUnitType(AdminUnitType adminUnitType)
        {
            _unitOfWork.AdminUnitTypeRepository.Add(adminUnitType);
            _unitOfWork.Save();
            return true;

        }
        public bool EditAdminUnitType(AdminUnitType adminUnitType)
        {
            _unitOfWork.AdminUnitTypeRepository.Edit(adminUnitType);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteAdminUnitType(AdminUnitType adminUnitType)
        {
            if (adminUnitType == null) return false;
            _unitOfWork.AdminUnitTypeRepository.Delete(adminUnitType);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.AdminUnitTypeRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.AdminUnitTypeRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<AdminUnitType> GetAllAdminUnitType()
        {
            return _unitOfWork.AdminUnitTypeRepository.GetAll();
        }
        public AdminUnitType FindById(int id)
        {
            return _unitOfWork.AdminUnitTypeRepository.FindById(id);
        }
        public List<AdminUnitType> FindBy(Expression<Func<AdminUnitType, bool>> predicate)
        {
            return _unitOfWork.AdminUnitTypeRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


