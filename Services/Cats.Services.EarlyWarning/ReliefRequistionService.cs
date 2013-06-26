

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;


namespace Cats.Services.EarlyWarning
{

    public class ReliefRequistionService:IReliefRequistionService
   {
       private readonly  IUnitOfWork _unitOfWork;
      

       public ReliefRequistionService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }
       #region Default Service Implementation
       public bool AddReliefRequistion(ReliefRequistion reliefRequistion)
       {
           _unitOfWork.ReliefRequistionRepository.Add(reliefRequistion);
           _unitOfWork.Save();
           return true;
           
       }
       public bool EditReliefRequistion(ReliefRequistion reliefRequistion)
       {
           _unitOfWork.ReliefRequistionRepository.Edit(reliefRequistion);
           _unitOfWork.Save();
           return true;

       }
         public bool DeleteReliefRequistion(ReliefRequistion reliefRequistion)
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
       public List<ReliefRequistion> GetAllReliefRequistion()
       {
           return _unitOfWork.ReliefRequistionRepository.GetAll();
       } 
       public ReliefRequistion FindById(int id)
       {
           return _unitOfWork.ReliefRequistionRepository.FindById(id);
       }
       public List<ReliefRequistion> FindBy(Expression<Func<ReliefRequistion, bool>> predicate)
       {
           return _unitOfWork.ReliefRequistionRepository.FindBy(predicate);
       }
        public IEnumerable<ReliefRequistion> Get(
          Expression<Func<ReliefRequistion, bool>> filter = null,
          Func<IQueryable<ReliefRequistion>, IOrderedQueryable<ReliefRequistion>> orderBy = null,
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
   
 
      
  