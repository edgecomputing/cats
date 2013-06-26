
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface IRoundService
    {

        bool AddRound(Round round);
        bool DeleteRound(Round round);
        bool DeleteById(int id);
        bool EditRound(Round round);
        Round FindById(int id);
        List<Round> GetAllRound();
        List<Round> FindBy(Expression<Func<Round, bool>> predicate);


    }
}


