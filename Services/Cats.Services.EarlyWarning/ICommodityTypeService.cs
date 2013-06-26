
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface ICommodityTypeService
    {

        bool AddCommodityType(CommodityType commodityType);
        bool DeleteCommodityType(CommodityType commodityType);
        bool DeleteById(int id);
        bool EditCommodityType(CommodityType commodityType);
        CommodityType FindById(int id);
        List<CommodityType> GetAllCommodityType();
        List<CommodityType> FindBy(Expression<Func<CommodityType, bool>> predicate);


    }
}



