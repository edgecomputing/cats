using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Data.Hub;
using Cats.Models.Hubs;

namespace Cats.Services.Hub
{
    public class StackEventTypeService:IStackEventTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StackEventTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
       public bool AddStackEventType(StackEventType entity)
       {
           _unitOfWork.StackEventTypeRepository.Add(entity);
           _unitOfWork.Save();
           return true;
           
       }
       public bool EditStackEventType(StackEventType entity)
       {
           _unitOfWork.StackEventTypeRepository.Edit(entity);
           _unitOfWork.Save();
           return true;

       }
         public bool DeleteStackEventType(StackEventType entity)
        {
             if(entity==null) return false;
           _unitOfWork.StackEventTypeRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
        }
       public  bool DeleteById(int id)
       {
           var entity = _unitOfWork.StackEventTypeRepository.FindById(id);
           if(entity==null) return false;
           _unitOfWork.StackEventTypeRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }
       public List<StackEventType> GetAllStackEventType()
       {
           return _unitOfWork.StackEventTypeRepository.GetAll();
       } 
       public StackEventType FindById(int id)
       {
           return _unitOfWork.StackEventTypeRepository.FindById(id);
       }
       public List<StackEventType> FindBy(Expression<Func<StackEventType, bool>> predicate)
       {
           return _unitOfWork.StackEventTypeRepository.FindBy(predicate);
       }
       #endregion
       
       public void Dispose()
       {
           _unitOfWork.Dispose();
           
       }
        public double GetFollowUpDurationByStackEventTypeId(int stackEventTypeId)
        {
            var followupDuration = _unitOfWork.StackEventTypeRepository.FindBy(s => s.StackEventTypeID == stackEventTypeId).Select(r => r.DefaultFollowUpDuration.Value).FirstOrDefault();
            return Convert.ToDouble(followupDuration);
        }

        
    }
}



