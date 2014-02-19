
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace Cats.Services.Logistics
{
   public  interface ITransferService
   {
       
       bool AddTransfer(Transfer transfer);
       bool DeleteTransfer(Transfer transfer);
       bool DeleteById(int id);
       bool EditTransfer(Transfer transfer);
       Transfer FindById(int id);
       List<Transfer> GetAllTransfer();
       List<Transfer> FindBy(Expression<Func<Transfer, bool>> predicate);
       bool Approve(Transfer transfer);


   }
}

          
      