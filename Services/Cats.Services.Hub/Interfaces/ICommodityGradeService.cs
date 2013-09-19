
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hub;

namespace Cats.Services.Hub
{
    public interface ICommodityGradeService : IDisposable
    {

        bool AddCommodityGrade(CommodityGrade commodityGrade);
        bool DeleteCommodityGrade(CommodityGrade commodityGrade);
        bool DeleteById(int id);
        bool EditCommodityGrade(CommodityGrade commodityGrade);
        CommodityGrade FindById(int id);
        List<CommodityGrade> GetAllCommodityGrade();
        List<CommodityGrade> FindBy(Expression<Func<CommodityGrade, bool>> predicate);


    }
}


