

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Services.Security;


namespace Cats.Services.EarlyWarning
{

    public class RegionalRequestDetailService : IRegionalRequestDetailService
    {
        private readonly IUnitOfWork _unitOfWork;


        public RegionalRequestDetailService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddRegionalRequestDetail(RegionalRequestDetail regionalRequestDetail)
        {
            _unitOfWork.RegionalRequestDetailRepository.Add(regionalRequestDetail);
            _unitOfWork.Save();
            return true;

        }
        public bool EditRegionalRequestDetail(RegionalRequestDetail regionalRequestDetail)
        {
            _unitOfWork.RegionalRequestDetailRepository.Edit(regionalRequestDetail);
            CalculateAllocation(regionalRequestDetail);
            _unitOfWork.Save();
            return true;

        }

        private bool CalculateAllocation(RegionalRequestDetail requestDetail)
        {
          
            foreach (var requestCommodity in requestDetail.RequestDetailCommodities)
            {
                var rationAmount = GetCommodityRation(requestDetail.RegionalRequestID, requestCommodity.CommodityID);
                var target = _unitOfWork.RequestDetailCommodityRepository.FindById(requestCommodity.RequestCommodityID);
                decimal ration = 0;
                ration = GetRationDependingOnPreference(rationAmount);
                target.Amount = requestDetail.Beneficiaries * ration;
            }
            return true;
        }
        public bool DeleteRegionalRequestDetail(RegionalRequestDetail regionalRequestDetail)
        {
            if (regionalRequestDetail == null) return false;
            _unitOfWork.RegionalRequestDetailRepository.Delete(regionalRequestDetail);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.RegionalRequestDetailRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.RegionalRequestDetailRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<RegionalRequestDetail> GetAllRegionalRequestDetail()
        {
            return _unitOfWork.RegionalRequestDetailRepository.GetAll();
        }
        public RegionalRequestDetail FindById(int id)
        {
            return _unitOfWork.RegionalRequestDetailRepository.FindById(id);
        }
        public List<RegionalRequestDetail> FindBy(Expression<Func<RegionalRequestDetail, bool>> predicate)
        {
            return _unitOfWork.RegionalRequestDetailRepository.FindBy(predicate);
        }
        public IEnumerable<RegionalRequestDetail> Get(
          Expression<Func<RegionalRequestDetail, bool>> filter = null,
          Func<IQueryable<RegionalRequestDetail>, IOrderedQueryable<RegionalRequestDetail>> orderBy = null,
          string includeProperties = "")
        {
            return _unitOfWork.RegionalRequestDetailRepository.Get(filter, orderBy, includeProperties);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }



        public bool Save()
        {
           _unitOfWork.Save();
            return true;
        }

       private decimal GetDurationOfAssistance(int planId, int woredaId)
       {
           var hrd =
               _unitOfWork.HRDDetailRepository.FindBy(h => h.HRD.PlanID == planId && h.AdminUnit.AdminUnitID == woredaId)
                   .FirstOrDefault();
           if (hrd != null) return hrd.DurationOfAssistance;
           return 0;
       }

       private decimal GetRationDependingOnPreference(decimal ration)
       {
             ration = ration / 1000; //change it to metric tone
             return ration;
           
        }
        public bool AddRequestDetailCommodity(int commodityId, int requestId)
        {
            var requestDetail = _unitOfWork.RegionalRequestDetailRepository.Get(t => t.RegionalRequestID == requestId);
            decimal DurationOfAssistance = 1;
            decimal ration = 0;
            var rationAmount = GetCommodityRation(requestId, commodityId);
           // ration = GetRationDependingOnPreference(rationAmount);

            foreach (var regionalRequestDetail in requestDetail)
            {
                
                //DurationOfAssistance = GetDurationOfAssistance(regionalRequestDetail.RegionalRequest.PlanID,
                //                                                regionalRequestDetail.Fdp.AdminUnitID);

               if(regionalRequestDetail.RequestDetailCommodities.All(t=>t.CommodityID!=commodityId))
               {
                   regionalRequestDetail.RequestDetailCommodities.Add(new RequestDetailCommodity
                                                                          {
                                                                              CommodityID=commodityId ,
                                                                              Amount = regionalRequestDetail.Beneficiaries * rationAmount 
                                                                              
                                                                          });
               }
            }
            _unitOfWork.Save();
            return true;
        }

        public bool AddAllCommodity(int regionalRequestID)
        {
            var regionalRequestDetail = _unitOfWork.RegionalRequestDetailRepository.Get(m => m.RegionalRequestID == regionalRequestID);
            var rationID = _unitOfWork.RegionalRequestRepository.FindById(regionalRequestID).RationID;
            var rationDetails = _unitOfWork.RationDetailRepository.FindBy(m => m.RationID == rationID);
           
                foreach (var requestDetail in regionalRequestDetail)
                {
                    foreach (var rationDetail in rationDetails)
                    {
                        decimal ration = 0;
                        ration = GetRationDependingOnPreference(rationDetail.Amount);
                        if (requestDetail.RequestDetailCommodities.All(t=>t.CommodityID!=rationDetail.CommodityID))
                        {
                            requestDetail.RequestDetailCommodities.Add(new RequestDetailCommodity()
                                {
                                    CommodityID = rationDetail.CommodityID,
                                    Amount = requestDetail.Beneficiaries * ration
                                });
                        }
                    }

                }
            _unitOfWork.Save();
            return true;


        }

        public bool AddRegionalRequestDetailWithBeneficiary(RegionalRequestDetail regionalRequestDetail)
        {
            var oldRequestDetail = _unitOfWork.RegionalRequestDetailRepository.FindBy(m => m.RegionalRequestID==regionalRequestDetail.RegionalRequestID
                                                                                      && m.Fdpid==regionalRequestDetail.Fdpid);
            if (oldRequestDetail.Count > 0) return false;
            _unitOfWork.RegionalRequestDetailRepository.Add(regionalRequestDetail);
            _unitOfWork.Save();
            return true;

        }

        public bool AddCommodityFdp(RegionalRequestDetail requestDetail)
        {
            if (AddRegionalRequestDetailWithBeneficiary(requestDetail))
            {


                var detail =_unitOfWork.RegionalRequestDetailRepository.FindBy(m => m.RegionalRequestID == requestDetail.RegionalRequestID).First();
                var commodityDetail =_unitOfWork.RequestDetailCommodityRepository.FindBy( m => m.RegionalRequestDetailID == detail.RegionalRequestDetailID);
                if (commodityDetail != null)
                {
                    foreach (var requestDetailCommodity in commodityDetail)
                    {
                        requestDetail.RequestDetailCommodities.Add(new RequestDetailCommodity
                            {
                                CommodityID = requestDetailCommodity.CommodityID,
                                Amount =
                                    requestDetail.Beneficiaries*
                                    GetCommodityRation(requestDetail.RegionalRequestID,
                                                       requestDetailCommodity.CommodityID)

                            });
                    }
                }
                _unitOfWork.Save();
                return true;
            }
            return false;
        }
        private decimal GetCommodityRation(int requestId, int commodityId)
        {
            var rationID = _unitOfWork.RegionalRequestRepository.FindById(requestId).RationID;
            var ration =
                _unitOfWork.RationDetailRepository.FindBy(t => t.RationID == rationID && t.CommodityID == commodityId).FirstOrDefault();
            if (ration == null) return 0;
            //return ration.Amount;
            return GetRationDependingOnPreference(ration.Amount);
        }
        public bool DeleteRequestDetailCommodity(int commodityId, int requestId)
        {
            var requestDetail = _unitOfWork.RegionalRequestDetailRepository.Get(t => t.RegionalRequestID == requestId);
            foreach (var regionalRequestDetail in requestDetail)
            {
               if(regionalRequestDetail.RequestDetailCommodities.Any(t=>t.CommodityID==commodityId))
               {
                   var requestDeatilId = regionalRequestDetail.RegionalRequestDetailID;
                   var requestCommodity =
                       _unitOfWork.RequestDetailCommodityRepository.Get(
                           t => t.CommodityID == commodityId && t.RegionalRequestDetailID == requestDeatilId).
                           FirstOrDefault();
                   _unitOfWork.RequestDetailCommodityRepository.Delete(requestCommodity);
               }
            }
            _unitOfWork.Save();
            return true;
        }
        public bool UpdateRequestDetailCommodity(int commodityId, int requestCommodityId)
        {
            var requestDetailCommodity = _unitOfWork.RequestDetailCommodityRepository.FindById( requestCommodityId);
            requestDetailCommodity.CommodityID = commodityId;
            var requestDetail =
                _unitOfWork.RegionalRequestDetailRepository.FindById(requestDetailCommodity.RegionalRequestDetailID);
          
            _unitOfWork.Save();  
            CalculateAllocation(requestDetail);
            return true;
        }

    }
}


