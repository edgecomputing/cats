
      using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
      using Cats.Models;

namespace Cats.Services.Common
{
    public interface ICommonService : IDisposable
    {
        IEnumerable<CommodityType> GetCommodityTypes(
         Expression<Func<CommodityType, bool>> filter = null,
         Func<IQueryable<CommodityType>, IOrderedQueryable<CommodityType>> orderBy = null,
         string includeProperties = "");

        IEnumerable<Commodity> GetCommodities(
          Expression<Func<Commodity, bool>> filter = null,
          Func<IQueryable<Commodity>, IOrderedQueryable<Commodity>> orderBy = null,
          string includeProperties = "");
        IEnumerable<Detail> GetDetails(
          Expression<Func<Detail, bool>> filter = null,
          Func<IQueryable<Detail>, IOrderedQueryable<Detail>> orderBy = null,
          string includeProperties = "");
        IEnumerable<Donor> GetDonors(
            Expression<Func<Donor, bool>> filter = null,
            Func<IQueryable<Donor>, IOrderedQueryable<Donor>> orderBy = null,
            string includeProperties = "");

        IEnumerable<Program> GetPrograms(
            Expression<Func<Program, bool>> filter = null,
            Func<IQueryable<Program>, IOrderedQueryable<Program>> orderBy = null,
            string includeProperties = "");
    }
}

          
      