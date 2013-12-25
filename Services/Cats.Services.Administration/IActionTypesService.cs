using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Administration
{
    public interface IActionTypesService
    {
        bool AddActionType(ActionTypes actionType);
        bool DeleteActionType(ActionTypes actionType);
        bool DeleteById(int id);
        bool EditActionType(ActionTypes actionType);
        ActionTypes FindById(int id);
        List<ActionTypes> GetAllActionType();
        List<ActionTypes> FindBy(Expression<Func<ActionTypes, bool>> predicate);
    }
}
