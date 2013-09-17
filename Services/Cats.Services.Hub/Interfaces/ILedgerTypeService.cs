
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hub;

namespace Cats.Services.Hub
{
    public interface ILedgerTypeService
    {

        bool AddLedgerType(LedgerType entity);
        bool DeleteLedgerType(LedgerType entity);
        bool DeleteById(int id);
        bool EditLedgerType(LedgerType entity);
        LedgerType FindById(int id);
        List<LedgerType> GetAllLedgerType();
        List<LedgerType> FindBy(Expression<Func<LedgerType, bool>> predicate);


    }
}


