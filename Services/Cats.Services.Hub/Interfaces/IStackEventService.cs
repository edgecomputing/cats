using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels;

namespace Cats.Services.Hub
{
   public interface IStackEventService
    {

       bool AddStackEvent(StackEvent entity);
       bool DeleteStackEvent(StackEvent entity);
       bool DeleteById(int id);
       bool EditStackEvent(StackEvent entity);
       StackEvent FindById(int id);
       List<StackEvent> GetAllStackEvent();
       List<StackEvent> FindBy(Expression<Func<StackEvent, bool>> predicate);

       List<StackEventLogViewModel> GetAllStackEvents(UserProfile user);
       List<StackEventLogViewModel> GetAllStackEventsByStoreIdStackId(UserProfile user, int StackId, int StoreId);
      
    }
}

      


      
