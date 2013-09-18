using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Data.Hub;
using Cats.Models.Hub;
using Cats.Models.Hub.ViewModels;

namespace Cats.Services.Hub
{
   public class StackEventService:IStackEventService
    {
       private readonly IUnitOfWork _unitOfWork;

       public StackEventService(IUnitOfWork unitOfWork)
       {
           _unitOfWork = unitOfWork; 
       }
       #region Default Service Implementation
       public bool AddStackEvent(StackEvent entity)
       {
           _unitOfWork.StackEventRepository.Add(entity);
           _unitOfWork.Save();
           return true;

       }
       public bool EditStackEvent(StackEvent entity)
       {
           _unitOfWork.StackEventRepository.Edit(entity);
           _unitOfWork.Save();
           return true;

       }
       public bool DeleteStackEvent(StackEvent entity)
       {
           if (entity == null) return false;
           _unitOfWork.StackEventRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }
       public bool DeleteById(int id)
       {
           var entity = _unitOfWork.StackEventRepository.FindById(id);
           if (entity == null) return false;
           _unitOfWork.StackEventRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }
       public List<StackEvent> GetAllStackEvent()
       {
           return _unitOfWork.StackEventRepository.GetAll();
       }
       public StackEvent FindById(int id)
       {
           return _unitOfWork.StackEventRepository.FindById(id);
       }
       public List<StackEvent> FindBy(Expression<Func<StackEvent, bool>> predicate)
       {
           return _unitOfWork.StackEventRepository.FindBy(predicate);
       }
       #endregion

       public void Dispose()
       {
           _unitOfWork.Dispose();

       }


      
        public List<StackEventLogViewModel> GetAllStackEvents(UserProfile user)
        {
             
            

            var StackEvents = _unitOfWork.StackEventRepository.Get();
            var events = (from c in StackEvents select new StackEventLogViewModel { EventDate = c.EventDate, StackEventType = c.StackEventType.Name, Description = c.Description, Recommendation = c.Recommendation, FollowUpDate = c.FollowUpDate.Value }).ToList();
                
            return events;
        }

        public List<StackEventLogViewModel> GetAllStackEventsByStoreIdStackId(UserProfile user, int StackId, int StoreId)
        {
            var stackEvents = _unitOfWork.StackEventRepository.Get(c => c.StackNumber == StackId && c.StoreID == StoreId);

            var events = (from c in stackEvents select new StackEventLogViewModel { EventDate = c.EventDate, StackEventType = c.StackEventType.Name, Description = c.Description, Recommendation = c.Recommendation, FollowUpDate = c.FollowUpDate.Value }).ToList();
            return events;
        }

       
    }
}







   
 
      
