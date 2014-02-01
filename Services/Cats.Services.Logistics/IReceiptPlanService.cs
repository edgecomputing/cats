using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;
namespace Cats.Services.Logistics
{
    public interface IReceiptPlanService
    {

        bool AddReceiptPlan(ReceiptPlan receiptPlan);
        bool DeleteReceiptPlan(ReceiptPlan receiptPlan);
        bool DeleteById(int id);
        bool EditReceiptPlan(ReceiptPlan receiptPlan);
        ReceiptPlan FindById(int id);
        List<ReceiptPlan> GetAllReceiptPlan();
        List<ReceiptPlan> FindBy(Expression<Func<ReceiptPlan, bool>> predicate);


    }
}


      
