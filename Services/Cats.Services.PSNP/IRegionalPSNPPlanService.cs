using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.PSNP
{
    public interface IRegionalPSNPPlanService
    {
        bool AddRegionalPSNPPlan(RegionalPSNPPlan item);
        bool UpdateRegionalPSNPPlan(RegionalPSNPPlan item);

        bool DeleteRegionalPSNPPlan(RegionalPSNPPlan item);
        bool DeleteById(int id);

        RegionalPSNPPlan FindById(int id);
        List<RegionalPSNPPlan> GetAllRegionalPSNPPlan();
        List<RegionalPSNPPlan> FindBy(Expression<Func<RegionalPSNPPlan, bool>> predicate);

        List<vwPSNPAnnualPlan> GetAnnualPlanRpt(int id);
        bool DoesPsnpPlanExistForThisRegion(int planId,int year);
        void AddPsnpPlan(string planName, DateTime startDate, DateTime endDate);
        RegionalPSNPPlan CreatePsnpPlan(int year,int duration,int ration,int statusID ,int planID);
        bool UpdatePsnpPlan(int year, int duration, int ration, int statusID, int planID);

    }
}