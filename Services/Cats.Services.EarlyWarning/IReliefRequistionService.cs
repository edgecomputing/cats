

      using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
      using Cats.Models;

namespace Cats.Services.EarlyWarning
{
   public  interface IReliefRequistionService
   {
       
       bool AddReliefRequistion(ReliefRequistion reliefRequistion);
       bool DeleteReliefRequistion(ReliefRequistion reliefRequistion);
       bool DeleteById(int id);
       bool EditReliefRequistion(ReliefRequistion reliefRequistion);
       ReliefRequistion FindById(int id);
       List<ReliefRequistion> GetAllReliefRequistion();
       List<ReliefRequistion> FindBy(Expression<Func<ReliefRequistion, bool>> predicate);

 IEnumerable<ReliefRequistion> Get(
           Expression<Func<ReliefRequistion, bool>> filter = null,
           Func<IQueryable<ReliefRequistion>, IOrderedQueryable<ReliefRequistion>> orderBy = null,
           string includeProperties = "");
   }
}


      

