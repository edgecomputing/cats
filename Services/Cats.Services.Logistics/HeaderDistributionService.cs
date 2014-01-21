using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Logistics
{
    public class HeaderDistributionService:IHeaderHeaderDistributionService
    {
        private readonly IUnitOfWork _unitOfWork;


        public HeaderDistributionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddHeaderDistribution(HeaderDistribution HeaderDistribution)
        {
            _unitOfWork.HeaderDistributionRepository.Add(HeaderDistribution);
            _unitOfWork.Save();
            return true;

        }
        public bool EditHeaderDistribution(HeaderDistribution HeaderDistribution)
        {
            _unitOfWork.HeaderDistributionRepository.Edit(HeaderDistribution);
            _unitOfWork.Save();
            return true;

        }

       

        public bool DeleteHeaderDistribution(HeaderDistribution HeaderDistribution)
        {
            if (HeaderDistribution == null) return false;
            _unitOfWork.HeaderDistributionRepository.Delete(HeaderDistribution);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.HeaderDistributionRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.HeaderDistributionRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<HeaderDistribution> GetAllHeaderDistribution()
        {
            return _unitOfWork.HeaderDistributionRepository.GetAll();
        }
        public HeaderDistribution FindById(int id)
        {
            return _unitOfWork.HeaderDistributionRepository.FindById(id);
        }
        public List<HeaderDistribution> FindBy(Expression<Func<HeaderDistribution, bool>> predicate)
        {
            return _unitOfWork.HeaderDistributionRepository.FindBy(predicate);
        }
        public IEnumerable<HeaderDistribution> Get(
           Expression<Func<HeaderDistribution, bool>> filter = null,
           Func<IQueryable<HeaderDistribution>, IOrderedQueryable<HeaderDistribution>> orderBy = null,
           string includeProperties = "")
        {
            return _unitOfWork.HeaderDistributionRepository.Get(filter, orderBy, includeProperties);
        }

        public List<ReliefRequisitionDetail> GetReliefRequisitions(int regionId)
        {
            return
                _unitOfWork.ReliefRequisitionDetailRepository.FindBy(r => r.ReliefRequisition.RegionID == regionId).
                    ToList();
        }

        public List<RegionalPSNPPlanDetail> GetPSNPPlan(int regionId)
        {
            return _unitOfWork.RegionalPSNPPlanDetailRepository.FindBy(p => p.RegionalPSNPPlan.RegionID == regionId).ToList();
        }

        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}
