
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface IRationDetailService : IDisposable
    {

        bool AddRationDetail(RationDetail rationDetail);
        bool DeleteRationDetail(RationDetail rationDetail);
        bool DeleteById(int id);
        bool EditRationDetail(RationDetail rationDetail);
        RationDetail FindById(int id);
        List<RationDetail> GetAllRationDetail();
        List<RationDetail> FindBy(Expression<Func<RationDetail, bool>> predicate);
        
        IEnumerable<RationDetail> Get(
                   Expression<Func<RationDetail, bool>> filter = null,
                   Func<IQueryable<RationDetail>, IOrderedQueryable<RationDetail>> orderBy = null,
                   string includeProperties = "");
    }
}


