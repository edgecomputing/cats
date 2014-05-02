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
       public bool ApproveRecieptPlan(LoanReciptPlan loanReciptPlan)
       {
           if (loanReciptPlan != null)
           {
               loanReciptPlan.StatusID = (int)LocalPurchaseStatus.Approved;
               _unitOfWork.LoanReciptPlanRepository.Edit(loanReciptPlan);
               foreach (var loan in loanReciptPlan.LoanReciptPlanDetails)
               {

                   var reciptAllocaltion = new ReceiptAllocation()
                       {
                           ReceiptAllocationID = Guid.NewGuid(),
                           ProgramID = loanReciptPlan.ProgramID,
                           CommodityID = loanReciptPlan.CommodityID,
                           ETA = loanReciptPlan.CreatedDate,
                           SINumber = loanReciptPlan.ShippingInstruction.Value,
                           QuantityInMT = loan.RecievedQuantity,
                           HubID = loan.HubID,
                           CommoditySourceID = loanReciptPlan.CommoditySourceID,
                           ProjectNumber = loanReciptPlan.ProjectCode,
                           //SourceHubID = loanReciptPlan.SourceHubID,
                           PartitionID = 0,
                           IsCommited = false
                       };

                   _unitOfWork.ReceiptAllocationReository.Add(reciptAllocaltion);
                   _unitOfWork.Save();
               }
               return true;

           }
           return false;
       }
       #endregion

       public void Dispose()
       {
           _unitOfWork.Dispose();
           
       }
       
   }
   }
   
         
      
