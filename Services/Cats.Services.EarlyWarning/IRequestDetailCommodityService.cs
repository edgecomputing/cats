using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    public interface IRequestDetailCommodityService
    {
        List<RequestDetailCommodity> FindBy(Expression<Func<RequestDetailCommodity, bool>> predicate);
    }
}
