using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace Cats.Services.Logistics
{
   public  interface ISupportTypeService
   {

       bool AddSupportType(SupportType supportType);
       bool DeleteSupportType(SupportType supportType);
       bool DeleteById(int id);
       bool EditSupportType(SupportType supportType);
       SupportType FindById(int id);
       List<SupportType> GetAllSupportType();
       List<SupportType> FindBy(Expression<Func<SupportType, bool>> predicate);


   }
}

