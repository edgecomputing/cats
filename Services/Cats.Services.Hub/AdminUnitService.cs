

using System;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hub;
using Cats.Models.Hub.Repository;
using Cats.Models.Hub.ViewModels.Report;


namespace Cats.Services.Hub
{

    public class AdminUnitService : IAdminUnitService
    {
        private readonly IUnitOfWork _unitOfWork;


        public AdminUnitService()
        {
            this._unitOfWork = new UnitOfWork();
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



        public const int WOREDATYPE = 4;
        public const int REGIONTYPE = 2;
        public const int ZONETYPE = 3;

        /// <summary>
        /// Gets the type of the by unit.
        /// </summary>
        /// <param name="typeId">The type id.</param>
        /// <returns> List<AdminUnit>returns>
        public List<AdminUnit> GetByUnitType(int typeId)
        {
            return _unitOfWork.AdminUnitRepository.FindBy(t => t.AdminUnitTypeID == typeId);
        }

        /// <summary>
        /// Gets the regions.
        /// </summary>
        /// <returns>List<AdminUnit></returns>
        public List<AdminUnit> GetRegions()
        {
            return _unitOfWork.AdminUnitRepository.FindBy(t => t.AdminUnitTypeID == 2);
        }

        /// <summary>
        /// Gets the region by zone id.
        /// </summary>
        /// <param name="zoneId">The zone id.</param>
        /// <returns></returns>
        public int GetRegionByZoneId(int zoneId)
        {
            return
                _unitOfWork.AdminUnitRepository.FindBy(t => t.AdminUnitTypeID == zoneId).
                Select(t => t.ParentID.Value).
                    Single();

        }

        /// <summary>
        /// Gets the admin unit types.
        /// </summary>
        /// <returns></returns>
        public List<AdminUnitType> GetAdminUnitTypes()
        {
            return _unitOfWork.AdminUnitTypeRepository.FindBy(t => t.AdminUnitTypeID > 1);

        }

        /// <summary>
        /// Gets the type of the admin unit.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public AdminUnitType GetAdminUnitType(int id)
        {
            return _unitOfWork.AdminUnitTypeRepository.Get(t => t.AdminUnitTypeID == id,null, "AdminUnits").SingleOrDefault();

        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <param name="unitId">The unit id.</param>
        /// <returns></returns>
        public List<AdminUnit> GetChildren(int unitId)
        {
            return _unitOfWork.AdminUnitRepository.Get(t => t.ParentID == unitId,null, "AdminUnit2").ToList();
           
        }

        /// <summary>
        /// Gets all woredas.
        /// </summary>
        /// <returns></returns>
        public List<AdminUnit> GetAllWoredas()
        {
            return
                _unitOfWork.AdminUnitRepository.Get(t => t.AdminUnitTypeID == WOREDATYPE, t => t.OrderBy(x => x.Name),
                                                    "AdminUnit2").ToList();
           
        }

        /// <summary>
        /// Gets the woredas by region.
        /// </summary>
        /// <param name="regionId">The region id.</param>
        /// <returns></returns>
        public List<AdminUnit> GetWoredasByRegion(int regionId)
        {
          return   _unitOfWork.AdminUnitRepository.Get(
                t => t.AdminUnitTypeID == WOREDATYPE && t.AdminUnit2.ParentID == regionId, t => t.OrderBy(x => x.Name),"AdminUnit2").
                ToList();
            
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<AreaViewModel> GetAllAreasForReport()
        {
            var tempAdminUnits = _unitOfWork.AdminUnitRepository.FindBy(t=>t.AdminUnitTypeID==2);
            var areas = (from c in tempAdminUnits select new AreaViewModel { AreaId = c.AdminUnitID, AreaName = c.Name }).ToList();
            return areas;
        }

        public List<AreaViewModel> GetZonesForReport(int AreaId)
        {
            var tempAdminUnits = _unitOfWork.AdminUnitRepository.FindBy(t => t.AdminUnitTypeID == 3 && t.ParentID==AreaId);
            var areas = (from c in tempAdminUnits select new AreaViewModel { AreaId = c.AdminUnitID, AreaName = c.Name }).ToList();
            return areas;
        }

        public List<AreaViewModel> GetWoredasForReport(int AreaId)
        {
            var tempAdminUnits = _unitOfWork.AdminUnitRepository.FindBy(t => t.AdminUnitTypeID == 4 && t.ParentID == AreaId);
            var Areas = (from c in tempAdminUnits select new AreaViewModel { AreaId = c.AdminUnitID, AreaName = c.Name }).ToList();
            return Areas;
        }

        public IEnumerable<TreeViewModel> GetTreeElts(int adminunitParentId, int hubId)
        {
            var UnclosedDispatchAllocations = _unitOfWork.DispatchAllocationRepository.FindBy(t =>
                                                                                      t.ShippingInstructionID.HasValue &&
                                                                                      t.ProjectCodeID.HasValue
                                                                                      && hubId == t.HubID &&
                                                                                      t.IsClosed == false);
                
          

            AdminUnit adminUnit = _unitOfWork.AdminUnitRepository.FindById(adminunitParentId);
            var parentAdminUnits = _unitOfWork.AdminUnitRepository.FindBy(t => t.ParentID == adminunitParentId);
            if (adminUnit.AdminUnitType.AdminUnitTypeID == 1)//by region
                return (from Unc in UnclosedDispatchAllocations
                        where Unc.FDP.AdminUnit.AdminUnit2.AdminUnit2.ParentID == adminunitParentId
                        group Unc by new { Unc.FDP.AdminUnit.AdminUnit2.AdminUnit2 } into b
                        select new TreeViewModel
                        {
                            Name = b.Key.AdminUnit2.Name,
                            Value = b.Key.AdminUnit2.AdminUnitID,
                            Enabled = true,
                            Count = b.Count()
                        }).Union(from ad in parentAdminUnits
                                 select new TreeViewModel
                                 {
                                     Name = ad.Name,
                                     Value = ad.AdminUnitID,
                                     Enabled = true,
                                     Count = 0
                                 }
                                                                        );
            else if (adminUnit.AdminUnitType.AdminUnitTypeID == 2)//by zone
                return
                                              (from Unc in UnclosedDispatchAllocations
                                               where Unc.FDP.AdminUnit.AdminUnit2.ParentID == adminunitParentId
                                               group Unc by new { Unc.FDP.AdminUnit.AdminUnit2 } into b
                                               select new TreeViewModel
                                               {
                                                   Name = b.Key.AdminUnit2.Name,
                                                   Value = b.Key.AdminUnit2.AdminUnitID,
                                                   Enabled = true,
                                                   Count = b.Count()
                                               }).Union(from ad in parentAdminUnits
                                                        select new TreeViewModel
                                                        {
                                                            Name = ad.Name,
                                                            Value = ad.AdminUnitID,
                                                            Enabled = true,
                                                            Count = 0
                                                        }
                                                                        );
            else //if (adminUnit.AdminUnitType.AdminUnitTypeID == 4)//by woreda
                return
                              (from Unc in UnclosedDispatchAllocations
                               where Unc.FDP.AdminUnit.ParentID == adminunitParentId
                               group Unc by new { Unc.FDP.AdminUnit } into b
                               select new TreeViewModel
                               {
                                   Name = b.Key.AdminUnit.Name,
                                   Value = b.Key.AdminUnit.AdminUnitID,
                                   Enabled = true,
                                   Count = b.Count()
                               }).Union(from ad in parentAdminUnits
                                        select new TreeViewModel
                                        {
                                            Name = ad.Name,
                                            Value = ad.AdminUnitID,
                                            Enabled = true,
                                            Count = 0
                                        }
         );


        }

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }



        public List<AdminUnit> Get(Expression<Func<AdminUnit, bool>> filter = null, Func<IQueryable<AdminUnit>, IOrderedQueryable<AdminUnit>> orderBy = null, string includeProperties = "")
        {
          return  _unitOfWork.AdminUnitRepository.Get(filter, orderBy, includeProperties).ToList();
        }
    }
}


