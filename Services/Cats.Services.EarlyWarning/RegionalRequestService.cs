

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;


namespace Cats.Services.EarlyWarning
{

    public class RegionalRequestService : IRegionalRequestService
   {
       private readonly  IUnitOfWork _unitOfWork;
      

       public RegionalRequestService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }
       #region Default Service Implementation
       public bool AddReliefRequistion(RegionalRequest reliefRequistion)
       {
           _unitOfWork.RegionalRequestRepository.Add(reliefRequistion);
           _unitOfWork.Save();
           return true;
           
       }
       public bool EditReliefRequistion(RegionalRequest reliefRequistion)
       {
           _unitOfWork.RegionalRequestRepository.Edit(reliefRequistion);
           _unitOfWork.Save();
           return true;

       }
         public bool DeleteReliefRequistion(RegionalRequest reliefRequistion)
        {
             if(reliefRequistion==null) return false;
           _unitOfWork.RegionalRequestRepository.Delete(reliefRequistion);
           _unitOfWork.Save();
           return true;
        }
       public  bool DeleteById(int id)
       {
           var entity = _unitOfWork.RegionalRequestRepository.FindById(id);
           if(entity==null) return false;
           _unitOfWork.RegionalRequestRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }
       public List<RegionalRequest> GetAllReliefRequistion()
       {
           return _unitOfWork.RegionalRequestRepository.GetAll();
       } 
       public RegionalRequest FindById(int id)
       {
           return _unitOfWork.RegionalRequestRepository.FindById(id);
       }
       public List<RegionalRequest> FindBy(Expression<Func<RegionalRequest, bool>> predicate)
       {
           return _unitOfWork.RegionalRequestRepository.FindBy(predicate);
       }
        public IEnumerable<RegionalRequest> Get(
          Expression<Func<RegionalRequest, bool>> filter = null,
          Func<IQueryable<RegionalRequest>, IOrderedQueryable<RegionalRequest>> orderBy = null,
          string includeProperties = "")
        {
            return _unitOfWork.RegionalRequestRepository.Get(filter, orderBy, includeProperties);
        }
       #endregion
       
       public void Dispose()
       {
           _unitOfWork.Dispose();
           
       }
       
   }
   }
   
 
      
  