
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hub;
using Cats.Models.Hub.Repository;
using Cats.Models.Hub.ViewModels.Report;

namespace Cats.Services.Hub
{
    public interface IAdminUnitService:IDisposable
    {

        bool AddAdminUnit(AdminUnit adminUnit);
        bool DeleteAdminUnit(AdminUnit adminUnit);
        bool DeleteById(int id);
        bool EditAdminUnit(AdminUnit adminUnit);

        AdminUnit FindById(int id);
        List<AdminUnit> GetAllAdminUnit();
        List<AdminUnit> Get(Expression<Func<AdminUnit,bool>> filter=null,Func<IQueryable<AdminUnit>,IOrderedQueryable<AdminUnit>> orderBy=null,string includeProperties=""  );
        List<AdminUnit> FindBy(Expression<Func<AdminUnit, bool>> predicate);

        List<AdminUnit> GetZonesByRegion(int regionId);


        List<AdminUnit> GetWoredasByZone(int zoneId);
        /// <summary>
        /// Gets the regions.
        /// </summary>
        /// <returns></returns>
        List<AdminUnit> GetRegions();
        /// <summary>
        /// Gets the region by zone id.
        /// </summary>
        /// <param name="zoneId">The zone id.</param>
        /// <returns></returns>
        int GetRegionByZoneId(int zoneId);
        /// <summary>
        /// Gets the admin unit types.
        /// </summary>
        /// <returns></returns>
        List<AdminUnitType> GetAdminUnitTypes();
        /// <summary>
        /// Gets the type of the admin unit.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        AdminUnitType GetAdminUnitType(int id);
        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <param name="unitId">The unit id.</param>
        /// <returns></returns>
        List<AdminUnit> GetChildren(int unitId);
        /// <summary>
        /// Gets all woredas.
        /// </summary>
        /// <returns></returns>
        List<AdminUnit> GetAllWoredas();
        /// <summary>
        /// Gets the woredas by region.
        /// </summary>
        /// <param name="regionId">The region id.</param>
        /// <returns></returns>
        List<AdminUnit> GetWoredasByRegion(int regionId);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<AreaViewModel> GetAllAreasForReport();

        IEnumerable<TreeViewModel> GetTreeElts(int p, int hubId);

        List<AreaViewModel> GetZonesForReport(int AreaId);

        List<AreaViewModel> GetWoredasForReport(int AreaId);

    }
}


