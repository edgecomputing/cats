
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.Logistics
{
   public  interface ILocalPurchaseService:IDisposable  
   {
       
       bool AddLocalPurchase(LocalPurchase localPurchase);
       bool DeleteLocalPurchase(LocalPurchase localPurchase);
       bool DeleteById(int id);
       bool EditLocalPurchase(LocalPurchase localPurchase);
       LocalPurchase FindById(int id);
       List<LocalPurchase> GetAllLocalPurchase();
       List<LocalPurchase> FindBy(Expression<Func<LocalPurchase, bool>> predicate);
       List<Models.Hub> GetAllHub();


   }
}

          
      