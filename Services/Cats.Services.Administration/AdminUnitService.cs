using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.ViewModels;

namespace Cats.Services.Administration
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

        public List<AdminUnit> GetRegions()
        {
            return _unitOfWork.AdminUnitRepository.Get(t => t.AdminUnitTypeID == 2).ToList();
        }
        public List<AdminUnit> GetZones(int regionId)
        {
            return _unitOfWork.AdminUnitRepository.Get(t => t.ParentID == regionId).ToList();
        }
        public List<AdminUnit> GetWoreda(int zoneId)
        {
            return _unitOfWork.AdminUnitRepository.Get(t => t.ParentID == zoneId).ToList();
        }

        public List<AdminUnit> GetAllRegions()
        {
            return _unitOfWork.AdminUnitRepository.Get(
                  t => t.AdminUnitTypeID == 2 && t.ParentID == 1, t => t.OrderBy(x => x.Name), "AdminUnit1, AdminUnit2").ToList();

        }

        public List<AdminUnit> GetZonesByRegion(int regionId)
        {
            return _unitOfWork.AdminUnitRepository.Get(
                  t => t.AdminUnitTypeID == 3 && t.ParentID == regionId, t => t.OrderBy(x => x.Name), "AdminUnit2").ToList();

        }

        public List<AdminUnit> GetWoredasByZone(int zoneId)
        {
            return _unitOfWork.AdminUnitRepository.Get(
                  t => t.AdminUnitTypeID == 4 && t.ParentID == zoneId, t => t.OrderBy(x => x.Name), "AdminUnit2").ToList();

        }

        

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}
