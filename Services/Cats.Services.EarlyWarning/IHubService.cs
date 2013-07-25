
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface IHubService
    {

        bool AddHub(Hub hub);
        bool DeleteHub(Hub hub);
        bool DeleteById(int id);
        bool EditHub(Hub hub);
        Hub FindById(int id);
        int GetHubId(string hub);
        List<Hub> GetAllHub();
        List<Hub> FindBy(Expression<Func<Hub, bool>> predicate);


    }
}


