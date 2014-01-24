using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace Cats.Services.Logistics
{
   public  interface IDistributionByAgeService:IDisposable
   {
       
       bool AddDistributionByAge(DistributionByAge distributionByAge);
       bool DeleteDistributionByAge(DistributionByAge distributionByAge);
       bool DeleteById(int id);
       bool EditDistributionByAge(DistributionByAge distributionByAge);
       DistributionByAge FindById(int id);
       List<DistributionByAge> GetAllDistributionByAge();
       List<DistributionByAge> FindBy(Expression<Func<DistributionByAge, bool>> predicate);




   }
}

         
