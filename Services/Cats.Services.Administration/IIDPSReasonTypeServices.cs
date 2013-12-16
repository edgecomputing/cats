using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Models;

namespace Cats.Services.Administration
{
    public  interface  IIDPSReasonTypeServices
    {
        bool AddIDPSReasonType(IDPSReasonType idpsReasonType);
        bool DeleteIDPSReasonType(IDPSReasonType idpsReasonType);
        bool DeleteById(int id);
        bool EditIDPSReasonType(IDPSReasonType idpsReasonType);
        IDPSReasonType FindById(int id);
        List<IDPSReasonType> GetAllIDPSReasonType();
        List<IDPSReasonType> FindBy(Expression<Func<IDPSReasonType, bool>> predicate);
    }
}
