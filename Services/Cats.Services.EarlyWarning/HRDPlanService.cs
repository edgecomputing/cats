using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
   public class HRDPlanService:IHRDPlanService
   {
       private IUnitOfWork _unitOfWork;

       public HRDPlanService(IUnitOfWork unitOfWork)
       {
           _unitOfWork = unitOfWork;
       }
       public bool AddHRDPlan(Models.Plan hrdPlan)
       {
           _unitOfWork.HrdPlanRepository.Add(hrdPlan);
           _unitOfWork.Save();
           return true;
        }

       public bool DeleteHRDPlan(Models.Plan hrdPlan)
        {
            if (hrdPlan == null) return false;
           _unitOfWork.HrdPlanRepository.Delete(hrdPlan);
           _unitOfWork.Save();
           return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.HrdPlanRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.HrdPlanRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditHRDPlan(Models.Plan hrdPlan)
        {
            _unitOfWork.HrdPlanRepository.Edit(hrdPlan);
            _unitOfWork.Save();
            return true;
        }

        public Models.Plan FindById(int id)
        {
            return _unitOfWork.HrdPlanRepository.FindById(id);
        }

        public List<Models.Plan> GetAllHRDPlan()
        {
            return _unitOfWork.HrdPlanRepository.GetAll();
        }

        public List<Models.Plan> FindBy(System.Linq.Expressions.Expression<Func<Models.Plan, bool>> predicate)
        {
            return _unitOfWork.HrdPlanRepository.FindBy(predicate);
        }

        public IEnumerable<Models.Plan> Get(System.Linq.Expressions.Expression<Func<Models.Plan, bool>> filter = null, Func<IQueryable<Models.Plan>, IOrderedQueryable<Models.Plan>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.HrdPlanRepository.Get(filter, orderBy, includeProperties);
        }
       public List<Program> GetPrograms()
       {
           return _unitOfWork.ProgramRepository.GetAll();
       }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
