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
    public class UtilizationHeaderSerivce:IUtilizationHeaderSerivce
    {
        private readonly IUnitOfWork _unitOfWork;


        public UtilizationHeaderSerivce(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddHeaderDistribution(UtilizationHeader HeaderDistribution)
        {
            _unitOfWork.UtilizationHeaderRepository.Add(HeaderDistribution);
            _unitOfWork.Save();
            return true;

        }
        public bool EditHeaderDistribution(UtilizationHeader HeaderDistribution)
        {
            _unitOfWork.UtilizationHeaderRepository.Edit(HeaderDistribution);
            _unitOfWork.Save();
            return true;

        }

       

        public bool DeleteHeaderDistribution(UtilizationHeader HeaderDistribution)
        {
            if (HeaderDistribution == null) return false;
            _unitOfWork.UtilizationHeaderRepository.Delete(HeaderDistribution);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.UtilizationHeaderRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.UtilizationHeaderRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<UtilizationHeader> GetAllHeaderDistribution()
        {
            return _unitOfWork.UtilizationHeaderRepository.GetAll();
        }
        public UtilizationHeader FindById(int id)
        {
            return _unitOfWork.UtilizationHeaderRepository.FindById(id);
        }
        public List<UtilizationHeader> FindBy(Expression<Func<UtilizationHeader, bool>> predicate)
        {
            return _unitOfWork.UtilizationHeaderRepository.FindBy(predicate);
        }
        public IEnumerable<UtilizationHeader> Get(
           Expression<Func<UtilizationHeader, bool>> filter = null,
           Func<IQueryable<UtilizationHeader>, IOrderedQueryable<UtilizationHeader>> orderBy = null,
           string includeProperties = "")
        {
            return _unitOfWork.UtilizationHeaderRepository.Get(filter, orderBy, includeProperties);
        }


      



        public List<ReliefRequisition> GetRequisitions(int zoneId, int programId, int planId ,int status,int month, int round)
        {
            switch (programId)
            {
                case (int) Cats.Models.Constant.Programs.Releif:
                    if (month!=-1 && round!=-1)
                    {
                        var requisition = _unitOfWork.ReliefRequisitionRepository.Get(r => r.ZoneID == zoneId &&
                                                                                           r.ProgramID == programId && 
                                                                                           r.RegionalRequest.PlanID == planId && 
                                                                                           r.RegionalRequest.Month == month && r.RegionalRequest.Round == round, null, null).ToList();
                        return requisition;
                    }
                    if (month!=-1 && round ==-1)
                    {
                        var requisition = _unitOfWork.ReliefRequisitionRepository.Get(r => r.ZoneID == zoneId &&
                                                                                           r.ProgramID == programId &&
                                                                                           r.RegionalRequest.PlanID == planId &&
                                                                                           r.RegionalRequest.Month == month, null, null).ToList();
                        return requisition;
                    }
                    if (month==-1 && round !=-1)
                    {
                        var requisition = _unitOfWork.ReliefRequisitionRepository.Get(r => r.ZoneID == zoneId &&
                                                                                           r.ProgramID == programId &&
                                                                                           r.RegionalRequest.PlanID == planId &&
                                                                                           r.RegionalRequest.Round == round, null, null).ToList();
                        return requisition;
                    }
                    return new List<ReliefRequisition>();
            }
            if (programId == (int) Cats.Models.Constant.Programs.PSNP)
            {
                if (round!=-1)
                {
                    var requisition = _unitOfWork.ReliefRequisitionRepository.Get(r => r.ZoneID == zoneId &&
                                                                                   r.ProgramID == programId &&
                                                                                   r.RegionalRequest.PlanID == planId &&
                                                                                   r.RegionalRequest.Round == round, null, null).ToList();
                    return requisition;
                }
                return new List<ReliefRequisition>();
            }
            return new List<ReliefRequisition>();
        }

        public List<ReliefRequisitionDetail> GetReliefRequisitions(int requisitionId)
        {
            var requisition = _unitOfWork.ReliefRequisitionDetailRepository.FindBy(r => r.RequisitionID == requisitionId).ToList();
            return requisition;
        }

        public RegionalPSNPPlan GetPSNPPlanRequisitions(int regionId, int status)
        {
            var psnpRequisition = _unitOfWork.RegionalPSNPPlanRepository.Get(p => p.RegionID == regionId && p.StatusID == status, null, null).FirstOrDefault();
            return psnpRequisition;
        }

        

        public RegionalPSNPPlan GetPSNPPlan(int planId)
        {
            var psnpRequisition = _unitOfWork.RegionalPSNPPlanRepository.Get(p => p.RegionalPSNPPlanID == planId, null, "RegionalPSNPPlanDetail").FirstOrDefault();
            return psnpRequisition;
        }

        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}
