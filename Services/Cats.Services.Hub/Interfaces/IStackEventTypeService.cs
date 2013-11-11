using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hubs;

namespace Cats.Services.Hub
{
    public interface IStackEventTypeService
    {

        bool AddStackEventType(StackEventType entity);
        bool DeleteStackEventType(StackEventType entity);
        bool DeleteById(int id);
        bool EditStackEventType(StackEventType entity);
        StackEventType FindById(int id);
        List<StackEventType> GetAllStackEventType();
        List<StackEventType> FindBy(Expression<Func<StackEventType, bool>> predicate);

        double GetFollowUpDurationByStackEventTypeId(int stackEventTypeId);
       

    }
}


     
      