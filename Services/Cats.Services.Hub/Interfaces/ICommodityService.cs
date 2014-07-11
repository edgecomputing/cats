
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels.Common;

namespace Cats.Services.Hub
{
    public interface ICommodityService : IDisposable
    {

        bool AddCommodity(Commodity commodity);
        bool DeleteCommodity(Commodity commodity);
        bool DeleteById(int id);
        bool EditCommodity(Commodity commodity);
        Commodity FindById(int id);
        List<Commodity> GetAllCommodity();
        List<Commodity> FindBy(Expression<Func<Commodity, bool>> predicate);

        /// <summary>
        /// Gets all parents.
        /// </summary>
        /// <returns></returns>
        List<Commodity> GetAllParents();
        /// <summary>
        /// Gets all sub commodities.
        /// </summary>
        /// <returns></returns>
        List<Commodity> GetAllSubCommodities();
        /// <summary>
        /// Gets the name of the commodity by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Commodity GetCommodityByName(string name);
        /// <summary>
        /// Gets all sub commodities by parant id.
        /// </summary>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        List<Commodity> GetAllSubCommoditiesByParantId(int Id);
        /// <summary>
        /// Determines whether name is valid for the specified commodity ID.
        /// </summary>
        /// <param name="CommodityID">The commodity ID.</param>
        /// <param name="Name">The name.</param>
        /// <returns>
        ///   <c>true</c> if  [name is valid] for [the specified commodity ID]; otherwise, <c>false</c>.
        /// </returns>
        bool IsNameValid(int? CommodityID, string Name);
        /// <summary>
        /// Determines whether [commodity code is valid] for [the specified commodity ID].
        /// </summary>
        /// <param name="CommodityID">The commodity ID.</param>
        /// <param name="CommodityCode">The commodity code.</param>
        /// <returns>
        ///   <c>true</c> if [commodity code is valid] for [the specified commodity ID]; otherwise, <c>false</c>.
        /// </returns>
        bool IsCodeValid(int? CommodityID, string CommodityCode);
        
        List<CommodityViewModel> GetAllCommodityForReprot();

    }
}


