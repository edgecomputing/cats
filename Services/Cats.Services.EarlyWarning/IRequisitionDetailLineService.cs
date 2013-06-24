
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace DRMFSS.BLL.Services
{
    public interface IRequisitionDetailLineService
    {

        bool AddRequisitionDetailLine(RequisitionDetailLine Entity);
        bool DeleteRequisitionDetailLine(RequisitionDetailLine Entity);
        bool DeleteById(int id);
        bool EditRequisitionDetailLine(RequisitionDetailLine Entity);
        RequisitionDetailLine FindById(int id);
        List<RequisitionDetailLine> GetAllRequisitionDetailLine();
        List<RequisitionDetailLine> FindBy(Expression<Func<RequisitionDetailLine, bool>> predicate);


    }
}


