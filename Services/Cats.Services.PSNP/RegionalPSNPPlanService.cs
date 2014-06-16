using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.Constant;

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
        public void AddPsnpPlan(string planName, DateTime startDate, DateTime endDate)
        {
            var oldPlan = _unitOfWork.PlanRepository.FindBy(m => m.PlanName == planName).SingleOrDefault();
            if (oldPlan == null)
            {
                var psnpProgram = _unitOfWork.ProgramRepository.FindBy(m => m.Name == "PSNP").SingleOrDefault();
                var plan = new Plan
                {
                    PlanName = planName,
                    StartDate = startDate,
                    EndDate = endDate,
                    Program = psnpProgram,
                    Status = (int)PlanStatus.PSNPCreated

                };
                _unitOfWork.PlanRepository.Add(plan);
                _unitOfWork.Save();
            }

        }
        public RegionalPSNPPlan CreatePsnpPlan(int year,int duration,int ration,int statusID ,int planID)
        {
            var psnp = new RegionalPSNPPlan()
                {
                    PlanId = planID,
                    Year = year,
                    Duration = duration,
                    RationID = ration,
                    StatusID = statusID
                };
            _unitOfWork.RegionalPSNPPlanRepository.Add(psnp);
            _unitOfWork.Save();
            return psnp;

        }
      public  bool UpdatePsnpPlan(int year, int duration, int ration, int statusID, int planID)

    {
        var psnp = new RegionalPSNPPlan()
        {
            PlanId = planID,
            Year = year,
            Duration = duration,
            RationID = ration,
            StatusID = statusID
        };
        _unitOfWork.RegionalPSNPPlanRepository.Edit(psnp);
        _unitOfWork.Save();
        return true;
    }

       
    }
}