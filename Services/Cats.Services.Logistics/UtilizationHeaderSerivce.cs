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
        public bool AddHeaderDistribution(WoredaStockDistribution HeaderDistribution)
        {
            _unitOfWork.WoredaStockDistributionRepository.Add(HeaderDistribution);
            _unitOfWork.Save();
            return true;

        }
        public bool EditHeaderDistribution(WoredaStockDistribution HeaderDistribution)
        {
            _unitOfWork.WoredaStockDistributionRepository.Edit(HeaderDistribution);
            _unitOfWork.Save();
            return true;

        }

       

        public bool DeleteHeaderDistribution(WoredaStockDistribution HeaderDistribution)
        {
            if (HeaderDistribution == null) return false;
            _unitOfWork.WoredaStockDistributionRepository.Delete(HeaderDistribution);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.WoredaStockDistributionRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.WoredaStockDistributionRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<WoredaStockDistribution> GetAllHeaderDistribution()
        {
            return _unitOfWork.WoredaStockDistributionRepository.GetAll();
        }
        public WoredaStockDistribution FindById(int id)
        {
            return _unitOfWork.WoredaStockDistributionRepository.FindById(id);
        }
        public List<WoredaStockDistribution> FindBy(Expression<Func<WoredaStockDistribution, bool>> predicate)
        {
            return _unitOfWork.WoredaStockDistributionRepository.FindBy(predicate);
        }
        public IEnumerable<WoredaStockDistribution> Get(
           Expression<Func<WoredaStockDistribution, bool>> filter = null,
           Func<IQueryable<WoredaStockDistribution>, IOrderedQueryable<WoredaStockDistribution>> orderBy = null,
           string includeProperties = "")
        {
            return _unitOfWork.WoredaStockDistributionRepository.Get(filter, orderBy, includeProperties);
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
            var psnpRequisition = _unitOfWork.RegionalPSNPPlanRepository.Get(p=> p.StatusID == status, null, null).FirstOrDefault();
            return psnpRequisition;
        }

        

        public RegionalPSNPPlan GetPSNPPlan(int planId)
        {
            var psnpRequisition = _unitOfWork.RegionalPSNPPlanRepository.Get(p => p.RegionalPSNPPlanID == planId, null, "RegionalPSNPPlanDetail").FirstOrDefault();
            return psnpRequisition;
        }

         public decimal GetTotalIn(int fdpId, int requisitionId)
         {
             var releifRequisition = _unitOfWork.ReliefRequisitionRepository.FindById(requisitionId);
             

             if (releifRequisition != null)
             {
                 var fdpReceipt =
                     _unitOfWork.DeliveryReconcileRepository.FindBy(r=>r.RequsitionNo == releifRequisition.RequisitionNo && r.FDPID == fdpId).Sum(s=>s.ReceivedAmount);
                 return fdpReceipt;
             }
             return 0;
        }

        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}
