
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hub;
using Cats.Models.Hub.ViewModels.Report;

namespace Cats.Services.Hub
{
    public interface IFDPService:IDisposable
    {

        bool AddFDP(FDP fdp);
        bool DeleteFDP(FDP fdp);
        bool DeleteById(int id);
        bool EditFDP(FDP fdp);
        FDP FindById(int id);
        List<FDP> GetAllFDP();
        List<FDP> FindBy(Expression<Func<FDP, bool>> predicate);

        /// <summary>
        /// Gets the FDPs by region.
        /// </summary>
        /// <param name="regionId">The region id.</param>
        /// <returns></returns>
        List<FDP> GetFDPsByRegion(int regionId);
        /// <summary>
        /// Gets the FDPs by woreda.
        /// </summary>
        /// <param name="woredaId">The woreda id.</param>
        /// <returns></returns>
        List<FDP> GetFDPsByWoreda(int woredaId);
        /// <summary>
        /// Gets the FDPs by zone.
        /// </summary>
        /// <param name="zoneId">The zone id.</param>
        /// <returns></returns>
        List<FDP> GetFDPsByZone(int zoneId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<AreaViewModel> GetAllFDPForReport();
    }
}


