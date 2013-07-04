

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.ViewModels;


namespace Cats.Services.EarlyWarning
{

    public class ReliefRequisitionService : IReliefRequisitionService
    {

        private readonly IUnitOfWork _unitOfWork;



        public ReliefRequisitionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddReliefRequisition(ReliefRequisition reliefRequisition)
        {
            _unitOfWork.ReliefRequisitionRepository.Add(reliefRequisition);
            _unitOfWork.Save();
            return true;

        }
        public bool EditReliefRequisition(ReliefRequisition reliefRequisition)
        {
            _unitOfWork.ReliefRequisitionRepository.Edit(reliefRequisition);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteReliefRequisition(ReliefRequisition reliefRequisition)
        {
            if (reliefRequisition == null) return false;
            _unitOfWork.ReliefRequisitionRepository.Delete(reliefRequisition);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.ReliefRequisitionRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ReliefRequisitionRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<ReliefRequisition> GetAllReliefRequisition()
        {
            return _unitOfWork.ReliefRequisitionRepository.GetAll();
        }
        public ReliefRequisition FindById(int id)
        {
            return _unitOfWork.ReliefRequisitionRepository.FindById(id);
        }
        public List<ReliefRequisition> FindBy(Expression<Func<ReliefRequisition, bool>> predicate)
        {
            return _unitOfWork.ReliefRequisitionRepository.FindBy(predicate);
        }

        public IEnumerable<ReliefRequisition> Get(
          Expression<Func<ReliefRequisition, bool>> filter = null,
          Func<IQueryable<ReliefRequisition>, IOrderedQueryable<ReliefRequisition>> orderBy = null,
          string includeProperties = "")
        {
            return _unitOfWork.ReliefRequisitionRepository.Get(filter, orderBy, includeProperties);
        }


        #endregion

        public List<ReliefRequisition> GetApprovedRequistion()
        {
            return new List<ReliefRequisition>(){
                new ReliefRequisition(){
                ProgramID=1,
                RegionID=1,
                RequestedBy=1,
                 RequestedDate=DateTime.Today,
                RequisitionNo="XYZ123",
                Round=1,
                Status=1,
                ZoneID=1,
                CommodityID=1
             

                }
            };
        }
        public void AddReliefRequisions(List<ReliefRequisition> reliefRequisitions)
        {
            foreach (var reliefRequisition in reliefRequisitions)
            {
                this._unitOfWork.ReliefRequisitionRepository.Add(reliefRequisition);
            }

        }

        public IEnumerable<ReliefRequisitionNew> CreateRequisition(int requestId)
        {
            //Check if Requisition is created from this request
            //
            var regionalRequest = _unitOfWork.RegionalRequestRepository.Get(t => t.RegionalRequestID == requestId && t.Status == (int)REGIONAL_REQUEST_STATUS.Submitted  , null, "RegionalRequestDetails").FirstOrDefault();
            if (regionalRequest == null) return null;

            var reliefRequistions = CreateRequistionFromRequest(regionalRequest);
            AddReliefRequisions(reliefRequistions);
            regionalRequest.Status = (int) REGIONAL_REQUEST_STATUS.Closed;
            _unitOfWork.Save();
            foreach (var item in reliefRequistions)
            {
                item.RequisitionNo = String.Format("REQ-{0}", item.RequisitionID);
            }
            _unitOfWork.Save();
           return  GetRequisitionByRequestId(requestId);

        }
       
        public ReliefRequisition GenerateRequisition(RegionalRequest regionalRequest, int commodityId, int zoneId)
        {

            var relifRequisition = new ReliefRequisition()
            {
                //TODO:Please Include Regional Request ID in Requisition 
                RegionalRequestID = regionalRequest.RegionalRequestID,
                Round = regionalRequest.Round,
                ProgramID = regionalRequest.ProgramId,
                CommodityID = commodityId,
                RequestedDate = DateTime.Today
                    //TODO:Please find another way how to specify Requistion No
                ,
                RequisitionNo = Guid.NewGuid().ToString(),
                RegionID = regionalRequest.RegionID,
                ZoneID = zoneId,
                Status = 1,
                //RequestedBy =itm.RequestedBy,
                //ApprovedBy=itm.ApprovedBy,
                //ApprovedDate=itm.ApprovedDate,

            };
            var relifRequistionDetail = (from requestDetail in regionalRequest.RegionalRequestDetails
                                         select new ReliefRequisitionDetail()
                                         {
                                             CommodityID = commodityId

                                             ,
                                             Amount = requestDetail.Grain
                                             ,
                                             BenficiaryNo = requestDetail.Beneficiaries
                                             ,
                                             FDPID = requestDetail.Fdpid

                                         }).ToList();
            relifRequisition.ReliefRequisitionDetails = relifRequistionDetail;

            return relifRequisition;

        }


        public List<ReliefRequisition> CreateRequistionFromRequest(RegionalRequest regionalRequest)
        {
            //Note Here we are going to create 4 requistion from one request
            //Assumtions Here is ColumnName of the request detail match with commodity name 
            //var regionalRequest = _regionalRequestService.Get(t => t.RegionalRequestID == requestId, null,
            //                                                  "RegionalRequestDetails").FirstOrDefault();
            //var regionalRequest = _regionalRequestService.GetAllReliefRequistion().FirstOrDefault();


            var regionalRequestDetailToGetCommodityId = new RegionalRequestDetail();
            var reliefRequisitions = new List<ReliefRequisition>();

            var zones = GetZonesFoodRequested(regionalRequest.RegionalRequestID);

            foreach (var zone in zones)
            {
                var zoneId = zone.HasValue ? zone.Value : -1;
                if (zoneId == -1) continue;

                //Create Requisiton for Grain

                var commodityId = _unitOfWork.CommodityRepository.FindBy(t => t.Name == regionalRequestDetailToGetCommodityId.GrainName).SingleOrDefault().CommodityID;

                reliefRequisitions.Add(GenerateRequisition(regionalRequest, commodityId, zoneId));


                //Create Requistion for Oil

                commodityId = _unitOfWork.CommodityRepository.FindBy(t => t.Name == regionalRequestDetailToGetCommodityId.OilName).SingleOrDefault().CommodityID;

                reliefRequisitions.Add(GenerateRequisition(regionalRequest, commodityId, zoneId));

                //Create Requistion for pulse

                commodityId = _unitOfWork.CommodityRepository.FindBy(t => t.Name == regionalRequestDetailToGetCommodityId.PulseName).SingleOrDefault().CommodityID;

                reliefRequisitions.Add(GenerateRequisition(regionalRequest, commodityId, zoneId));

                //Create Requistion for CSB

                commodityId = _unitOfWork.CommodityRepository.FindBy(t => t.Name == regionalRequestDetailToGetCommodityId.CSBName).SingleOrDefault().CommodityID;
                reliefRequisitions.Add(GenerateRequisition(regionalRequest, commodityId, zoneId));
            }

            return reliefRequisitions;
        }
        public List<int?> GetZonesFoodRequested(int requestId)
        {
            var regionalRequestDetails =
                _unitOfWork.RegionalRequestDetailRepository.Get(t => t.RegionalRequestID == requestId, null,
                                                                "FDP,FDP.AdminUnit");
            var zones =
                (from requestDetail in regionalRequestDetails
                 where requestDetail.Fdp.AdminUnit.ParentID != null
                 select requestDetail.Fdp.AdminUnit.ParentID).Distinct();
            return zones.ToList();

        }

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }





        public bool Save()
        {
            _unitOfWork.Save();
            return true;
        }





        public bool AssignRequisitonNo(int requisitonId, string requisitonNo)
        {
            var requisition = _unitOfWork.ReliefRequisitionRepository.FindById(requisitonId);
            requisition.RequisitionNo = requisitonNo;
           
            return true;
        }

        public IEnumerable<ReliefRequisitionNew>  GetRequisitionByRequestId(int requestId)
        {
            var reliefRequistions =
               _unitOfWork.ReliefRequisitionRepository.Get(t => t.RegionalRequestID == requestId, null, "Program,AdminUnit1,AdminUnit.AdminUnit2,Commodity").ToList();

            var input = (from itm in reliefRequistions
                         orderby itm.ZoneID
                         select new ReliefRequisitionNew()
                         {
                             //TODO:Include navigation property for commodity on relife requistion
                             Commodity = itm.Commodity.Name,
                             Program = itm.Program.Name,
                             Region = itm.AdminUnit1.Name,
                             Round = itm.Round,
                             Zone = itm.AdminUnit.Name,
                             Status = (int)REGIONAL_REQUEST_STATUS.Draft,
                             RequisitionID = itm.RequisitionID,
                             // RequestedBy = itm.UserProfile,
                             // ApprovedBy = itm.ApprovedBy,
                             RequestedDate = itm.RequestedDate,
                             ApprovedDate = itm.ApprovedDate,

                             Input = new ReliefRequisitionNew.ReliefRequisitionNewInput()
                             {
                                 Number = itm.RequisitionID,
                                 RequisitionNo = itm.RequisitionNo
                             }
                         });
            return input;
        }
    }
}


