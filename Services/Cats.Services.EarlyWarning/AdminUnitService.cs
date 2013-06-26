

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;


namespace Cats.Services.EarlyWarning
{

    public class AdminUnitService : IAdminUnitService
    {
        private readonly IUnitOfWork _unitOfWork;


        public AdminUnitService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddAdminUnit(AdminUnit adminUnit)
        {
            _unitOfWork.AdminUnitRepository.Add(adminUnit);
            _unitOfWork.Save();
            return true;

        }
        public bool EditAdminUnit(AdminUnit adminUnit)
        {
            _unitOfWork.AdminUnitRepository.Edit(adminUnit);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteAdminUnit(AdminUnit adminUnit)
        {
            if (adminUnit == null) return false;
            _unitOfWork.AdminUnitRepository.Delete(adminUnit);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.AdminUnitRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.AdminUnitRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<AdminUnit> GetAllAdminUnit()
        {
            return _unitOfWork.AdminUnitRepository.GetAll();
        }
        public AdminUnit FindById(int id)
        {
            return _unitOfWork.AdminUnitRepository.FindById(id);
        }
        public List<AdminUnit> FindBy(Expression<Func<AdminUnit, bool>> predicate)
        {
            return _unitOfWork.AdminUnitRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}



