using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;
using Cats.Models.ViewModels;

namespace Cats.Services.PSNP
{
    public interface IRegionalPSNPPledgeService
    {
        bool AddRegionalPSNPPledge(RegionalPSNPPledge item);
        bool UpdateRegionalPSNPPledge(RegionalPSNPPledge item);

        bool DeleteRegionalPSNPPledge(RegionalPSNPPledge item);
        bool DeleteById(int id);

        RegionalPSNPPledge FindById(int id);
        List<RegionalPSNPPledge> GetAllRegionalPSNPPledge();
        List<RegionalPSNPPledge> FindBy(Expression<Func<RegionalPSNPPledge, bool>> predicate);
    }
}
