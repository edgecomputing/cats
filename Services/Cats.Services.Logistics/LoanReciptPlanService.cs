using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.Constant;

namespace Cats.Services.Logistics
{

    public class LoanReciptPlanService:ILoanReciptPlanService
   {
       private readonly  IUnitOfWork _unitOfWork;
      

       public LoanReciptPlanService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }
       #region Default Service Implementation
       public bool AddLoanReciptPlan(LoanReciptPlan entity)
       {
           _unitOfWork.LoanReciptPlanRepository.Add(entity);
           _unitOfWork.Save();
           return true;
           
       }
       public bool EditLoanReciptPlan(LoanReciptPlan entity)
       {
           _unitOfWork.LoanReciptPlanRepository.Edit(entity);
           _unitOfWork.Save();
           return true;

       }
         public bool DeleteLoanReciptPlan(LoanReciptPlan entity)
        {
             if(entity==null) return false;
           _unitOfWork.LoanReciptPlanRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
        }
       public  bool DeleteById(int id)
       {
           var entity = _unitOfWork.LoanReciptPlanRepository.FindById(id);
           if(entity==null) return false;
           _unitOfWork.LoanReciptPlanRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }
       public List<LoanReciptPlan> GetAllLoanReciptPlan()
       {
           return _unitOfWork.LoanReciptPlanRepository.GetAll();
       } 
       public LoanReciptPlan FindById(int id)
       {
           return _unitOfWork.LoanReciptPlanRepository.FindById(id);
       }
       public List<LoanReciptPlan> FindBy(Expression<Func<LoanReciptPlan, bool>> predicate)
       {
           return _unitOfWork.LoanReciptPlanRepository.FindBy(predicate);
       }
       #endregion

       public void Dispose()
       {
           _unitOfWork.Dispose();
           
       }
       
   }
   }
   
         
      
