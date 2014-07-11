
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace DRMFSS.BLL.Services
{
    public interface IReliefRequistionService
    {

        bool AddReliefRequistion(ReliefRequistion Entity);
        bool DeleteReliefRequistion(ReliefRequistion Entity);
        bool DeleteById(int id);
        bool EditReliefRequistion(ReliefRequistion Entity);
        ReliefRequistion FindById(int id);
        List<ReliefRequistion> GetAllReliefRequistion();
        List<ReliefRequistion> FindBy(Expression<Func<ReliefRequistion, bool>> predicate);


    }
}


