
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels.Report;

namespace Cats.Services.Hub
{
    public interface ICommoditySourceService
    {

        bool AddCommoditySource(CommoditySource commoditySource);
        bool DeleteCommoditySource(CommoditySource commoditySource);
        bool DeleteById(int id);
        bool EditCommoditySource(CommoditySource commoditySource);
        CommoditySource FindById(int id);
        List<CommoditySource> GetAllCommoditySource();
        List<CommoditySource> FindBy(Expression<Func<CommoditySource, bool>> predicate);

        /// <summary>
        /// return all commodity types in CommoditySourceViewModel format 
        /// </summary>
        /// <returns></returns>
        List<CommoditySourceViewModel> GetAllCommoditySourceForReport();
    }
}


