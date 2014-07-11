using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.Logistics
{
   public  interface ILocalPurchaseDetailService:IDisposable
   {
       
       bool AddLocalPurchaseDetail(LocalPurchaseDetail localPurchaseDetail);
       bool DeleteLocalPurchaseDetail(LocalPurchaseDetail localPurchaseDetail);
       bool DeleteById(int id);
       bool EditLocalPurchaseDetail(LocalPurchaseDetail localPurchaseDetail);
       LocalPurchaseDetail FindById(int id);
       List<LocalPurchaseDetail> GetAllLocalPurchaseDetail();
       List<LocalPurchaseDetail> FindBy(Expression<Func<LocalPurchaseDetail, bool>> predicate);


   }
}

          
      