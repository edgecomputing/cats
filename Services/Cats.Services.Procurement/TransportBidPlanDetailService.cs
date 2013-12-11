using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.ViewModels;

namespace Cats.Services.Procurement
{
    public class TransportBidPlanDetailService : ITransportBidPlanDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransportBidPlanDetailService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool AddTransportBidPlanDetail(TransportBidPlanDetail item)
        {
            _unitOfWork.TransportBidPlanDetailRepository.Add(item);
            _unitOfWork.Save();
            return true;
        }
        public bool UpdateTransportBidPlanDetail(TransportBidPlanDetail item)
        {
            if (item == null) return false;
            _unitOfWork.TransportBidPlanDetailRepository.Edit(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteTransportBidPlanDetail(TransportBidPlanDetail item)
        {
            if (item == null) return false;
            _unitOfWork.TransportBidPlanDetailRepository.Delete(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var item = _unitOfWork.TransportBidPlanDetailRepository.FindById(id);
            return DeleteTransportBidPlanDetail(item);
        }
        public bool DeleteByBidPlanID(int id)
        {
            var bidPlan = _unitOfWork.TransportBidPlanDetailRepository.FindBy(m => m.BidPlanID == id);
            if (bidPlan!=null)
            {
                foreach (var transportBidPlanDetail in bidPlan)
                {
                    _unitOfWork.TransportBidPlanDetailRepository.Delete(transportBidPlanDetail);
                    _unitOfWork.Save();                    
                }
                return true;
            }
            return false;

        }
        public TransportBidPlanDetail FindById(int id)
        {
            return _unitOfWork.TransportBidPlanDetailRepository.FindById(id);
        }
        public List<TransportBidPlanDetail> GetAllTransportBidPlanDetail()
        {
            return _unitOfWork.TransportBidPlanDetailRepository.GetAll();

        }
        public List<TransportBidPlanDetail> FindBy(Expression<Func<TransportBidPlanDetail, bool>> predicate)
        {
            return _unitOfWork.TransportBidPlanDetailRepository.FindBy(predicate);

        }
        public double GetRegionPlanTotal(int bidplanid, int regionId, int programId)
        {
            List<TransportBidPlanDetail> bidDetails = this.GetAllTransportBidPlanDetail();
            decimal r=
            (from planDetail in bidDetails
             where planDetail.ProgramID == programId && planDetail.BidPlanID==bidplanid && planDetail.Destination.AdminUnit2.AdminUnit2.AdminUnitID==regionId
                 select planDetail.Quantity).Sum();

            return (double)r;
        }


        public decimal GetHrdCommodityAmount(int woredaID)
        {
            var hrd = _unitOfWork.HRDRepository.FindBy(m => m.Status == 4).FirstOrDefault();
            decimal totalAmout=0;
            decimal totalCommodity=0;
            if (hrd!=null)
            {   
                
               var ration = _unitOfWork.RationRepository.FindBy(m => m.RationID == hrd.RationID).FirstOrDefault();
                if (ration!=null)
                {
                    totalAmout = ration.RationDetails.Sum(m => m.Amount);
                }
                
                //var hrdDetail = hrd.HRDDetails.FirstOrDefault(m => m.WoredaID == woredaID);
                foreach (var hrdDetail in hrd.HRDDetails.Where(m=>m.WoredaID==woredaID))
                {
                    totalCommodity = totalAmout * hrdDetail.NumberOfBeneficiaries * hrdDetail.DurationOfAssistance;
                }
               
                return totalCommodity;
            }
            return 0;
        }
        public decimal GetWoredaGroupedPsnpAmount(int woredaID)
        {
            var allResult = GetPsnpCommodityAmount();
            if (allResult != null)
            {
                var totalCommodity= allResult.FirstOrDefault(m => m.WoredaID == woredaID);
                if (totalCommodity != null) return totalCommodity.TotalAmount;
            }
            return 0;
        }
        public List<PSNPCommodityAmmountViewModel> GetPsnpCommodityAmount()
        {
            var applicationSettingPsnp = _unitOfWork.ApplicationSettingRepository.FindBy(m => m.SettingName == "PSNPWorkflow").FirstOrDefault();
            decimal rationTotalAmout = 0;
            if (applicationSettingPsnp != null)
            {
                var planID = Int32.Parse(applicationSettingPsnp.SettingValue);
                var psnpPlan = _unitOfWork.RegionalPSNPPlanRepository.FindBy(m => m.PlanId == planID);
               
                var result=new List<PSNPCommodityAmmountViewModel>();
                if (psnpPlan!=null)
                {
                    foreach(var psnpDetail in psnpPlan)
                    {
                        var ration =_unitOfWork.RationRepository.FindBy(m => m.RationID == psnpDetail.RationID).FirstOrDefault();
                        if (ration!=null)
                        {
                            rationTotalAmout = ration.RationDetails.Sum(m => m.Amount);
                        }
                        var psnpPlanDetail = psnpDetail.RegionalPSNPPlanDetails;
                        var woredaGroup = (from groupedPsnp in psnpPlanDetail
                                           group groupedPsnp by groupedPsnp.PlanedFDP.AdminUnit
                                           into woredaDtail
                                           select new
                                               {
                                                   woreda = woredaDtail.Key,
                                                   numberOfBeneficiary = woredaDtail.Sum(m => m.BeneficiaryCount),
                                                   foodRation=woredaDtail.First().FoodRatio,
                                                   detail = woredaDtail
                                               });
                        decimal amout = rationTotalAmout;
                        result = (from woredaDetail in woredaGroup
                                  select new PSNPCommodityAmmountViewModel
                                      {
                                          WoredaID = woredaDetail.woreda.AdminUnitID,
                                          TotalAmount = woredaDetail.numberOfBeneficiary * woredaDetail.foodRation * amout

                                      }).ToList();

                   
                    }
                    return result;
                }
            }

            return null;
        }
        
    }
}