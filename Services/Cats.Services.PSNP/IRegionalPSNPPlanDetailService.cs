using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.PSNP
{
    public interface IRegionalPSNPPlanDetailService
    {
        bool AddRegionalPSNPPlanDetail(RegionalPSNPPlanDetail item);
        bool UpdateRegionalPSNPPlanDetail(RegionalPSNPPlanDetail item);

        bool DeleteRegionalPSNPPlanDetail(RegionalPSNPPlanDetail item);
        bool DeleteById(int id);

        RegionalPSNPPlanDetail FindById(int id);
        List<RegionalPSNPPlanDetail> GetAllRegionalPSNPPlanDetail();
        List<RegionalPSNPPlanDetail> FindBy(Expression<Func<RegionalPSNPPlanDetail, bool>> predicate);
        
        
    }
}