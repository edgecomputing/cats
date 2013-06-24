
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace DRMFSS.BLL.Services
{
    public interface IReliefRequisitionDetailService
    {

        bool AddReliefRequisitionDetail(ReliefRequisitionDetail Entity);
        bool DeleteReliefRequisitionDetail(ReliefRequisitionDetail Entity);
        bool DeleteById(int id);
        bool EditReliefRequisitionDetail(ReliefRequisitionDetail Entity);
        ReliefRequisitionDetail FindById(int id);
        List<ReliefRequisitionDetail> GetAllReliefRequisitionDetail();
        List<ReliefRequisitionDetail> FindBy(Expression<Func<ReliefRequisitionDetail, bool>> predicate);


    }
}


      

