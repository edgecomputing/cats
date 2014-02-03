using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.Logistics
{
    public interface IReceiptPlanDetailService
    {

        bool AddReceiptPlanDetail(ReceiptPlanDetail receiptPlanDetail);
        bool DeleteReceiptPlanDetail(ReceiptPlanDetail receiptPlanDetail);
        bool DeleteById(int id);
        bool EditReceiptPlanDetail(ReceiptPlanDetail receiptPlanDetail);
        ReceiptPlanDetail FindById(int id);
        List<ReceiptPlanDetail> GetAllReceiptPlanDetail();
        List<ReceiptPlanDetail> FindBy(Expression<Func<ReceiptPlanDetail, bool>> predicate);
        List<ReceiptPlanDetail> GetNewReceiptPlanDetail();

    }
}


      
