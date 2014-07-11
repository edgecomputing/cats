using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.Logistics
{
   public  interface ILoanReciptPlanDetailService
   {
       
       bool AddLoanReciptPlanDetail(LoanReciptPlanDetail loanReciptPlanDetail);
       bool DeleteLoanReciptPlanDetail(LoanReciptPlanDetail loanReciptPlanDetail);
       bool DeleteById(int id);
       bool EditLoanReciptPlanDetail(LoanReciptPlanDetail loanReciptPlanDetail);
       LoanReciptPlanDetail FindById(int id);
       List<LoanReciptPlanDetail> GetAllLoanReciptPlanDetail();
       List<LoanReciptPlanDetail> FindBy(Expression<Func<LoanReciptPlanDetail, bool>> predicate);
       decimal GetRemainingQuantity(int id);
       bool AddRecievedLoanReciptPlanDetail(LoanReciptPlanDetail loanReciptPlanDetail);

   }
}

          
      