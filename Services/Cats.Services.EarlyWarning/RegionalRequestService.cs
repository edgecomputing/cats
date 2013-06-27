

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
           _unitOfWork.ReliefRequistionRepository.Add(reliefRequistion);
           _unitOfWork.Save();
           return true;
           
       }
       public bool EditReliefRequistion(RegionalRequest reliefRequistion)
       {
           _unitOfWork.ReliefRequistionRepository.Edit(reliefRequistion);
           _unitOfWork.Save();
           return true;

       }
         public bool DeleteReliefRequistion(RegionalRequest reliefRequistion)
        {
             if(reliefRequistion==null) return false;
           _unitOfWork.ReliefRequistionRepository.Delete(reliefRequistion);
           _unitOfWork.Save();
           return true;
        }
       public  bool DeleteById(int id)
       {
           var entity = _unitOfWork.ReliefRequistionRepository.FindById(id);
           if(entity==null) return false;
           _unitOfWork.ReliefRequistionRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }
       public List<RegionalRequest> GetAllReliefRequistion()
       {
           return _unitOfWork.ReliefRequistionRepository.GetAll();
       } 
       public RegionalRequest FindById(int id)
       {
           return _unitOfWork.ReliefRequistionRepository.FindById(id);
       }
       public List<RegionalRequest> FindBy(Expression<Func<RegionalRequest, bool>> predicate)
       {
           return _unitOfWork.ReliefRequistionRepository.FindBy(predicate);
       }
        public IEnumerable<RegionalRequest> Get(
          Expression<Func<RegionalRequest, bool>> filter = null,
          Func<IQueryable<RegionalRequest>, IOrderedQueryable<RegionalRequest>> orderBy = null,
          string includeProperties = "")
        {
            return _unitOfWork.ReliefRequistionRepository.Get(filter, orderBy, includeProperties);
        }
       #endregion
       
       public void Dispose()
       {
           _unitOfWork.Dispose();
           
       }
       
   }
   }
   
 
      
  