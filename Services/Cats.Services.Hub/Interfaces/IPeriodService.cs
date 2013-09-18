
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models.Hub;

namespace Cats.Services.Hub
{
    public interface IPeriodService
    {

        bool AddPeriod(Period entity);
        bool DeletePeriod(Period entity);
        bool DeleteById(int id);
        bool EditPeriod(Period entity);
        Period FindById(int id);
        List<Period> GetAllPeriod();
        List<Period> FindBy(Expression<Func<Period, bool>> predicate);
         List<int?> GetYears();
         List<int?> GetMonths(int year);
        Period GetPeriod(int year, int month);

    }
}


