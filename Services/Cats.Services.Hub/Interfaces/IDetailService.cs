
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels.Common;

namespace Cats.Services.Hub
{
    public interface IDetailService
    {

        bool AddDetail(Detail detail);
        bool DeleteDetail(Detail detail);
        bool DeleteById(int id);
        bool EditDetail(Detail detail);
        Detail FindById(int id);
        List<Detail> GetAllDetail();
        List<Detail> FindBy(Expression<Func<Detail, bool>> predicate);

        /// <summary>
        /// Gets the List of Details by master ID.
        /// </summary>
        /// <param name="masterId">The master id.</param>
        /// <returns></returns>
        IQueryable<Detail> GetByMasterID(int masterId);
        List<ReasonViewModel> GetReasonByMaster(int masterId);
    }
}


