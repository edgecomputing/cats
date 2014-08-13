using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.Constant;

namespace Cats.Services.EarlyWarning
{
   public class PlanService:IPlanService
   {
       private IUnitOfWork _unitOfWork;

       public PlanService(IUnitOfWork unitOfWork)
       {
           _unitOfWork = unitOfWork;
       }
       public bool AddPlan(Models.Plan plan)
       {
           _unitOfWork.PlanRepository.Add(plan);
           _unitOfWork.Save();
           return true;
        }

       public bool DeletePlan(Models.Plan plan)
        {
            if (plan == null) return false;
            _unitOfWork.PlanRepository.Delete(plan);
           _unitOfWork.Save();
           return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.PlanRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.PlanRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditPlan(Models.Plan plan)
        {
            _unitOfWork.PlanRepository.Edit(plan);
            _unitOfWork.Save();
            return true;
        }

        public Models.Plan FindById(int id)
        {
            return _unitOfWork.PlanRepository.FindById(id);
        }

        public List<Models.Plan> GetAllPlan()
        {
            return _unitOfWork.PlanRepository.GetAll();
        }

        public List<Models.Plan> FindBy(System.Linq.Expressions.Expression<Func<Models.Plan, bool>> predicate)
        {
            return _unitOfWork.PlanRepository.FindBy(predicate);
        }

        public IEnumerable<Models.Plan> Get(System.Linq.Expressions.Expression<Func<Models.Plan, bool>> filter = null, Func<IQueryable<Models.Plan>, IOrderedQueryable<Models.Plan>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.PlanRepository.Get(filter, orderBy, includeProperties);
        }
        public void AddNeedAssessmentPlan(NeedAssessment needAssessment)
        {
            var oldPlan = _unitOfWork.PlanRepository.FindBy(m => m.PlanName==needAssessment.Plan.PlanName).SingleOrDefault();
            if (oldPlan == null)
            {
                var program = _unitOfWork.ProgramRepository.FindBy(m => m.Name == "Relief");
                var plan = new Plan
                    {
                        PlanName = needAssessment.Plan.PlanName,
                        StartDate = needAssessment.Plan.StartDate,
                        EndDate = needAssessment.Plan.EndDate,
                        ProgramID = program.First().ProgramID,
                        Program = _unitOfWork.ProgramRepository.FindBy(m=>m.Name=="Relief").FirstOrDefault()
                        
                    };
                
                var savePlan = _unitOfWork.PlanRepository.Add(plan);
                _unitOfWork.Save();
                //if (!savePlan)
                //{
                //    return null;
                //}
                //return plan;
            }
            //return oldPlan;
        }
       public List<Program> GetPrograms()
       {
           return _unitOfWork.ProgramRepository.GetAll();
       }
       public List<Program> GetNonReliefProgram()
       {
           return _unitOfWork.ProgramRepository.FindBy(m => m.ProgramID != 1);
       }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }


        public void AddPlan(string planName, DateTime startDate, DateTime endDate)
        {
            var oldPlan = _unitOfWork.PlanRepository.FindBy(m => m.PlanName == planName).SingleOrDefault();
            if(oldPlan == null)
            {
                var reliefProgram = _unitOfWork.ProgramRepository.FindBy(m => m.Name == "Relief").SingleOrDefault();
                var plan = new Plan
                {
                    PlanName = planName,
                    StartDate = startDate,
                    EndDate = endDate,
                    Program = reliefProgram,
                    Status = (int)PlanStatus.Draft
                    
                };
                _unitOfWork.PlanRepository.Add(plan);
                _unitOfWork.Save();
            }
            AssessmentPlanStatus(oldPlan);
        }


        public void AddHRDPlan(string planName, DateTime startDate, DateTime endDate)
        {
               var oldPlan = _unitOfWork.PlanRepository.FindBy(m => m.PlanName == planName).SingleOrDefault();
               if (oldPlan == null)
               {
                   var reliefProgram = _unitOfWork.ProgramRepository.FindBy(m => m.Name == "Relief").SingleOrDefault();
                   var plan = new Plan
                       {
                           PlanName = planName,
                           StartDate = startDate,
                           EndDate = endDate,
                           Program = reliefProgram,
                           Status = (int) PlanStatus.HRDCreated

                       };
                   _unitOfWork.PlanRepository.Add(plan);
                   _unitOfWork.Save();
               }

        }


        public List<NeedAssessment> PlannedNeedAssessment(int planID)
        {
            var needAssessment = _unitOfWork.NeedAssessmentRepository.FindBy(m => m.PlanID == planID).ToList();
            if (needAssessment != null)
                return needAssessment;
            return null;

        }

        public List<HRD> PlannedHRD(int planID)
        {
            var hrd = _unitOfWork.HRDRepository.FindBy(m => m.PlanID == planID);
            if (hrd != null)
                return hrd;
            return null;

        }


        public void ChangePlanStatus(int planID)
        {
            var plan = _unitOfWork.PlanRepository.FindById(planID);
            if(plan!=null)
            {
                plan.Status = (int) PlanStatus.PSNPCreated;
                _unitOfWork.PlanRepository.Edit(plan);
                _unitOfWork.Save();
            }
        }
       public void AssessmentPlanStatus(Plan plan)
       {
           if (plan !=null)
           {
               plan.Status = (int)PlanStatus.AssessmentCreated;
               _unitOfWork.PlanRepository.Edit(plan);
               _unitOfWork.Save();
           }
       }
       //public void HRDPlanStatus(Plan plan)
       //{
       //    if (plan != null)
       //    {
       //        plan.Status = (int)PlanStatus.HRDCreated;
       //        _unitOfWork.PlanRepository.Edit(plan);
       //        _unitOfWork.Save();
       //    }
       //}
   }
}
