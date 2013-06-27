
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;

namespace DRMFSS.BLL.Services
{
    public interface IHubService
    {

        bool AddHub(Hub hub);
        bool DeleteHub(Hub hub);
        bool DeleteById(int id);
        bool EditHub(Hub hub);
        Hub FindById(int id);
        List<Hub> GetAllHub();
        List<Hub> FindBy(Expression<Func<Hub, bool>> predicate);


    }
}


