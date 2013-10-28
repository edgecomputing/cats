

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.Constant;
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

        //public List<ReliefRequisition> getApp(int Status)
        //{
        //     var Request =  _unitOfWork.RegionalRequestDetailRepository.Get(t => t.RegionalRequest.Status == Status, null,
        //                                                        "RegionalRequest,AdminUnit");
        //    var requestList = (from requestDetail in regionalRequestDetails
        //         where requestDetail.Fdp.AdminUnit.ParentID != null
        //         select requestDetail.Fdp.AdminUnit.ParentID).Distinct();
        //    return zones.ToList();

        //}
         
        public List<ReliefRequisition> GetApprovedRequistions()
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
        
        public List<RegionalRequisitionsSummary> GetRequisitionsSentToLogistics()
        {
           var app = _unitOfWork.ReliefRequisitionRepository.FindBy(t => t.Status == 2 );
           var ds =
                (
                    from a in app
                    group a by new { a.RegionID,a.AdminUnit.Name} into regionalRequistions
                    select new
                            {
                                regionalRequistions.Key.Name,
                                ProgramBased = from a in regionalRequistions
                                               group a by a.ProgramID into final
                                               select new 
                                               RegionalRequisitionsSummary()
                                               {
                                                    RegionID = regionalRequistions.Key.RegionID,
                                                    RegionName = regionalRequistions.Key.Name,
                                                    DateLastModified = regionalRequistions.Select(d=>d.ApprovedDate).Last(),
                                                    NumberOfHubAssignedRequisitions = final.Count(r=>r.Status>=2),
                                                    NumberOfTotalRequisitions = regionalRequistions.Count(),
                                                    ProgramType = final.Key.ToString(),
                                                    Percentage = (final.Count(r=>r.Status==3)/regionalRequistions.Count())*100
                                               }
                            }
                );
            
            var result = new List<RegionalRequisitionsSummary>();

            foreach (var d in ds)
            {
                foreach (var e in d.ProgramBased)
                {
                    
                    //var t = d.ProgramBased.Take(e);
                    
                    var t = new RegionalRequisitionsSummary()
                    {
                        RegionID = e.RegionID,
                        RegionName = e.RegionName,
                        DateLastModified = e.DateLastModified,
                        NumberOfHubAssignedRequisitions = e.NumberOfHubAssignedRequisitions,
                        NumberOfTotalRequisitions = e.NumberOfTotalRequisitions,
                        ProgramType = e.ProgramType,
                        Percentage = e.Percentage
                    };

                    //result.Add(new RegionalRequisitionsSummary()
                    //    {
                    //        RegionID = 1,
                    //        RegionName = "Afar",
                    //        DateLastModified = DateTime.Now,
                    //        NumberOfHubAssignedRequisitions = 45,
                    //        NumberOfTotalRequisitions = 80,
                    //        Percentage = (45 / 80) * 100,
                    //        ProgramType = e.ProgramType
                    //    });

                    result.Add(t);
                }
            }

            return result;
            
            
            
            //foreach (var d in ds)
            //{
            //    var region = "";
            //    var program = "";

            //    foreach (var reliefRequisition in d)
            //    {
            //        region = reliefRequisition.Program.Name;
            //        program = reliefRequisition.Program.Name;
            //    } 

            //    var temp = new RegionalRequisitionsSummary()
            //        {
            //            RegionID = d.Key.RegionID,
            //            RegionName = region,
            //            NumberOfHubAssignedRequisitions = d.Count(),
            //            NumberOfTotalRequisitions = d.Count(),
            //            Percentage = d.Count(),
            //            DateLastModified = DateTime.Now,
            //            ProgramType = d.Key.ProgramID
            //        };

            //result.Add(temp);
            //}

            //return result;



            //select new RegionalRequisitionsSummary()
            //   {
            //       RegionName = "",
            //       RegionID = g.Key,
            //       NumberOfHubAssignedRequisitions = g.Count(),
            //       NumberOfTotalRequisitions = g.Count(),
            //       Percentage = g.Count(),
            //       DateLastModified = DateTime.Now,
            //       ProgramType = "PSNP"
            //   }
            //);

            //return d.ToList();

            //(from app in approved  )
            //return new List<RegionalRequisitionsSummary>(){
            //    new RegionalRequisitionsSummary(){
            //        RegionID = 1,
            //        RegionName = "Afar",
            //        NumberOfHubAssignedRequisitions = 15,
            //        NumberOfTotalRequisitions = 50,
            //        Percentage = 30,
            //        DateLastModified = DateTime.Now
            //    },
            //    new RegionalRequisitionsSummary(){
            //        RegionID = 2,
            //        RegionName = "Tigray",
            //        NumberOfHubAssignedRequisitions = 15,
            //        NumberOfTotalRequisitions = 50,
            //        Percentage = 30,
            //        DateLastModified = DateTime.Now
            //    }
            //};
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
            var regionalRequest = _unitOfWork.RegionalRequestRepository.Get(t => t.RegionalRequestID == requestId && t.Status == (int)RegionalRequestStatus.Approved  , null, "RegionalRequestDetails").FirstOrDefault();
            if (regionalRequest == null) return null;
            
            var reliefRequistions = CreateRequistionFromRequest(regionalRequest);
            AddReliefRequisions(reliefRequistions);
            regionalRequest.Status = (int)RegionalRequestStatus.Closed;
            _unitOfWork.Save();
            
            foreach (var item in reliefRequistions)
            {
                item.RequisitionNo = String.Format("REQ-{0}", item.RequisitionID);
            }
            _unitOfWork.Save();
            return  GetRequisitionByRequestId(requestId);
        }
       
        public ReliefRequisition GenerateRequisition(RegionalRequest regionalRequest, List<RegionalRequestDetail> regionalRequestDetails, int commodityId, int zoneId)
        {

            var relifRequisition = new ReliefRequisition()
            {
                //TODO:Please Include Regional Request ID in Requisition 
                RegionalRequestID = regionalRequest.RegionalRequestID,
                Round = regionalRequest.Month,
                ProgramID = regionalRequest.ProgramId,
                CommodityID = commodityId,
                RequestedDate = DateTime.Today
                
                //TODO:Please find another way how to specify Requistion No
                ,
                RequisitionNo = Guid.NewGuid().ToString(),
                RegionID = regionalRequest.RegionID,
                ZoneID = zoneId,
                Status = (int)ReliefRequisitionStatus.Draft,
                //RequestedBy =itm.RequestedBy,
                //ApprovedBy=itm.ApprovedBy,
                //ApprovedDate=itm.ApprovedDate,

            };

            foreach (var regionalRequestDetail in regionalRequestDetails)
            {
                var relifRequistionDetail = new ReliefRequisitionDetail();
                var commodity = regionalRequestDetail.RequestDetailCommodities.First(t => t.CommodityID == commodityId);
                relifRequistionDetail.DonorID = regionalRequest.DonorID;
                relifRequistionDetail.FDPID = regionalRequestDetail.Fdpid;
                relifRequistionDetail.BenficiaryNo = regionalRequestDetail.Beneficiaries;
                relifRequistionDetail.CommodityID = commodity.CommodityID;
                relifRequistionDetail.Amount = commodity.Amount;
                relifRequisition.ReliefRequisitionDetails.Add(relifRequistionDetail);
            }
            return relifRequisition;
        }


        public List<ReliefRequisition> CreateRequistionFromRequest(RegionalRequest regionalRequest)
        {
            //Note Here we are going to create 4 requistion from one request
            //Assumtions Here is ColumnName of the request detail match with commodity name 
            var requestDetails =
                _unitOfWork.RegionalRequestDetailRepository.Get(
                    t => t.RegionalRequestID == regionalRequest.RegionalRequestID,null,"FDP.AdminUnit");
          
            var reliefRequisitions = new List<ReliefRequisition>();

            var zones = GetZonesFoodRequested(regionalRequest.RegionalRequestID);
          
            foreach (var zone in zones)
            {
                var zoneId = zone.HasValue ? zone.Value : -1;
                if (zoneId == -1) continue;
                var zoneRequestDetails = (from item in requestDetails where item.Fdp.AdminUnit.ParentID == zoneId select item).ToList();
                if(zoneRequestDetails.Count < 1) continue;

                var requestDetail = zoneRequestDetails.FirstOrDefault();
                if(requestDetail==null) continue;

                var requestCommodity=
                    (from item in requestDetail.RequestDetailCommodities select item.CommodityID ).ToList();
                if (requestCommodity.Count<1) continue;

                foreach (var commodityId in requestCommodity)
                {
                    reliefRequisitions.Add(GenerateRequisition(regionalRequest, zoneRequestDetails,commodityId, zoneId));
                }
               
               
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





        //public bool Save()
        //{
        //    _unitOfWork.Save();
        //    return true;
        //}





        public bool AssignRequisitonNo(Dictionary<int , string> requisitionNumbers )
        {

            foreach (var requisitonNumber in requisitionNumbers)
            {
                var requisition = _unitOfWork.ReliefRequisitionRepository.FindById(requisitonNumber.Key);
                requisition.RequisitionNo = requisitonNumber.Value;
              

            }

            _unitOfWork.Save();
           
           
           
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
                             Month = itm.Month,
                             Zone = itm.AdminUnit.Name,
                             Status = itm.Status,
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


        public bool EditAllocatedAmount(Dictionary<int,decimal> allocations )
        {

            foreach (var alloction in allocations)
            {
                var requisitionDetail = _unitOfWork.ReliefRequisitionDetailRepository.FindById(alloction.Key);
                if (requisitionDetail == null) return false;
                requisitionDetail.Amount = alloction.Value;

             
            }

           _unitOfWork.Save();

            return true;
        }
    }
}


