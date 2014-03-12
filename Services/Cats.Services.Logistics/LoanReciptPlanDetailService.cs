using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Services.Logistics;


namespace Cats.Services.Logistics
{

    public class LoanReciptPlanDetailService:ILoanReciptPlanDetailService
   {
       private readonly  IUnitOfWork _unitOfWork;
      

       public LoanReciptPlanDetailService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }
       #region Default Service Implementation
       public bool AddLoanReciptPlanDetail(LoanReciptPlanDetail entity)
       {
           _unitOfWork.LoanReciptPlanDetailRepository.Add(entity);
           _unitOfWork.Save();
           return true;
           
       }
       public bool EditLoanReciptPlanDetail(LoanReciptPlanDetail entity)
       {
           _unitOfWork.LoanReciptPlanDetailRepository.Edit(entity);
           _unitOfWork.Save();
           return true;

       }
         public bool DeleteLoanReciptPlanDetail(LoanReciptPlanDetail entity)
        {
             if(entity==null) return false;
           _unitOfWork.LoanReciptPlanDetailRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
        }
       public  bool DeleteById(int id)
       {
           var entity = _unitOfWork.LoanReciptPlanDetailRepository.FindById(id);
           if(entity==null) return false;
           _unitOfWork.LoanReciptPlanDetailRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }
       public List<LoanReciptPlanDetail> GetAllLoanReciptPlanDetail()
       {
           return _unitOfWork.LoanReciptPlanDetailRepository.GetAll();
       } 
       public LoanReciptPlanDetail FindById(int id)
       {
           return _unitOfWork.LoanReciptPlanDetailRepository.FindById(id);
       }
       public List<LoanReciptPlanDetail> FindBy(Expression<Func<LoanReciptPlanDetail, bool>> predicate)
       {
           return _unitOfWork.LoanReciptPlanDetailRepository.FindBy(predicate);
       }
       #endregion
       public decimal GetRemainingQuantity(int id)
       {
           var reciptPlan = _unitOfWork.LoanReciptPlanRepository.FindById(id);
           decimal totalRecived = 0;
           if (reciptPlan!=null)
           {
               totalRecived += reciptPlan.LoanReciptPlanDetails.Sum(loanreciptPlanDetail => loanreciptPlanDetail.RecievedQuantity);

               //decimal totalRecieved = reciptPlan.LoanReciptPlanDetails.Sum(reciptPlanDetail => reciptPlanDetail.RecievedQuantity);
              return reciptPlan.Quantity - totalRecived;
           }
           return 0;
       }
        public bool AddRecievedLoanReciptPlanDetail(LoanReciptPlanDetail loanReciptPlanDetail)
        {
            var loanReciptPlan = _unitOfWork.LoanReciptPlanRepository.FindById(loanReciptPlanDetail.LoanReciptPlanID);
            if (loanReciptPlan!=null)
            {
                _unitOfWork.LoanReciptPlanDetailRepository.Add(loanReciptPlanDetail);
                var reciptAllocaltion = new ReceiptAllocation()
                    {
                        ReceiptAllocationID = Guid.NewGuid(),
                        PartitionID = 0,
                        IsCommited = false,
                        ETA = loanReciptPlan.CreatedDate,
                        ProjectNumber = loanReciptPlan.ProjectCode,
                        CommodityID = loanReciptPlan.CommodityID,
                        CommoditySourceID = loanReciptPlan.CommoditySourceID,
                        SINumber = loanReciptPlan.ShippingInstruction.Value,
                        QuantityInMT = loanReciptPlanDetail.RecievedQuantity,
                        HubID = loanReciptPlanDetail.HubID,
                        //SourceHubID = loanReciptPlan.SourceHubID,
                        ProgramID = loanReciptPlan.ProgramID,
                        IsClosed = false
                    };
                _unitOfWork.ReceiptAllocationReository.Add(reciptAllocaltion);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }
       public void Dispose()
       {
           _unitOfWork.Dispose();
           
       }
       
   }
   }
   
         
      