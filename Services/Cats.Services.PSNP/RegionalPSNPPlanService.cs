using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.PSNP
{
    public class RegionalPSNPPlanService : IRegionalPSNPPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegionalPSNPPlanService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool AddRegionalPSNPPlan(RegionalPSNPPlan item)
        {
            _unitOfWork.RegionalPSNPPlanRepository.Add(item);
            _unitOfWork.Save();
            return true;
        }
        public bool UpdateRegionalPSNPPlan(RegionalPSNPPlan item)
        {
            if (item == null) return false;
            _unitOfWork.RegionalPSNPPlanRepository.Edit(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteRegionalPSNPPlan(RegionalPSNPPlan item)
        {
            if (item == null) return false;
            _unitOfWork.RegionalPSNPPlanRepository.Delete(item);
            _unitOfWork.Save();
            return true;
        }
        
        public bool DeleteById(int id)
        {
            var item = _unitOfWork.RegionalPSNPPlanRepository.FindById(id);
            return DeleteRegionalPSNPPlan(item);
        }
        
        public RegionalPSNPPlan FindById(int id)
        {
            return _unitOfWork.RegionalPSNPPlanRepository.FindById(id);
        }

        public List<RegionalPSNPPlan> GetAllRegionalPSNPPlan()
        {
            return _unitOfWork.RegionalPSNPPlanRepository.GetAll();

        }
        public List<RegionalPSNPPlan> FindBy(Expression<Func<RegionalPSNPPlan, bool>> predicate)
        {
            return _unitOfWork.RegionalPSNPPlanRepository.FindBy(predicate);
        }


        public List<vwPSNPAnnualPlan> GetAnnualPlanRpt(int id)
        {
            return _unitOfWork.VwPSNPAnnualPlanRepository.FindBy(t=>t.RegionalPSNPPlanID==id);
        }

        public bool DoesPsnpPlanExistForThisRegion(int planId,int year)
        {
            var psnp =
                 _unitOfWork.RegionalPSNPPlanRepository.Get(p => p.PlanId == planId && p.Year==year).Count();
            return psnp > 0;
        }

       
    }
}