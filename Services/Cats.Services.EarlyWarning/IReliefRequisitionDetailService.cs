
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface IReliefRequisitionDetailService
    {

        bool AddReliefRequisitionDetail(ReliefRequisitionDetail reliefRequisitionDetail);
        bool DeleteReliefRequisitionDetail(ReliefRequisitionDetail reliefRequisitionDetail);
        bool DeleteById(int id);
        bool EditReliefRequisitionDetail(ReliefRequisitionDetail reliefRequisitionDetail);
        ReliefRequisitionDetail FindById(int id);
        List<ReliefRequisitionDetail> GetAllReliefRequisitionDetail();
        List<ReliefRequisitionDetail> FindBy(Expression<Func<ReliefRequisitionDetail, bool>> predicate);
        IEnumerable<ReliefRequisitionDetail> Get(
           Expression<Func<ReliefRequisitionDetail, bool>> filter = null,
           Func<IQueryable<ReliefRequisitionDetail>, IOrderedQueryable<ReliefRequisitionDetail>> orderBy = null,
           string includeProperties = "");


    }
}


