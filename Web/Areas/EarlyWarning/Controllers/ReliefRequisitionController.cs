using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Models;
using Cats.Services.EarlyWarning;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class ReliefRequisitionController : Controller
    {
        //
        // GET: /EarlyWarning/ReliefRequisition/

        private readonly IReliefRequisitionService _reliefRequisitionService;
        private readonly IRegionalRequestService _regionalRequestService;
        private readonly ICommodityService _commodityService;

        public ReliefRequisitionController(IReliefRequisitionService reliefRequisitionService
            , IRegionalRequestService regionalRequestService
            , ICommodityService commodityService)
        {
            this._reliefRequisitionService = reliefRequisitionService;
            this._regionalRequestService = regionalRequestService;
            this._commodityService = commodityService;

        }

        public ViewResult Requistions()
        {
            var releifRequistions = _reliefRequisitionService.GetAllReliefRequisition();

            return View(releifRequistions);
        }

        [HttpGet]
        public ViewResult NewRequisiton(int id)
        {
            //Check if Requisition is created from this request

            var regionalRequest = _regionalRequestService.FindById(id);
            if(regionalRequest ==null ) return null;

            var reliefRequistions = CreateRequistionFromRequest(id);
            foreach (var reliefRequisition in reliefRequistions)
            {
                _reliefRequisitionService.AddReliefRequisition(reliefRequisition);
            }
            _reliefRequisitionService.Save();


            var input = (from itm in reliefRequistions
                         select new ReliefRequisitionNew()
                         {
                             CommodityID  = itm.CommodityID,
                             ProgramID = itm.ProgramID,
                             RegionID = itm.RegionID,
                             Round = itm.Round,
                             ZoneID = itm.ZoneID,
                             Status=itm.Status,
                             ReliefRequisitionID =itm.ReliefRequisitionID,
                             RequestedBy =itm.RequestedBy,
                             ApprovedBy=itm.ApprovedBy,
                             RequisitionDate = itm.RequisitionDate,
                             ApprovedDate=itm.ApprovedDate,
                             Input = new ReliefRequisitionNew.ReliefRequisitionNewInput()
                             {
                                 Number = itm.ReliefRequisitionID,
                                RequisitionNo=itm.RequisitionNo
                             }
                         });
            return View(input);
            
          
        }

        [HttpPost]
        public ActionResult NewRequisiton(List<ReliefRequisitionNew.ReliefRequisitionNewInput> inputs )
        {
            var requId = 0;
            foreach (var reliefRequisitionNewInput in inputs)
            {

                var tempReliefRequisiton =
                    _reliefRequisitionService.FindById(reliefRequisitionNewInput.Number);
             //   requId = tempReliefRequistionDetail.RegionalRequestID;
                tempReliefRequisiton.RequisitionNo = reliefRequisitionNewInput.RequisitionNo;
              

            }
            _reliefRequisitionService.Save();



            return RedirectToAction("Requistions", "ReliefRequisition");
        }
        public  List<ReliefRequisition> CreateRequistionFromRequest(int requestId)
        {
            //Note Here we are going to create 4 requistion from one request
            //Assumtions Here is ColumnName of the request detail match with commodity name 
            var regionalRequest = _regionalRequestService.Get(t => t.RegionalRequestID == requestId, null,
                                                              "RegionalRequestDetails").FirstOrDefault();
              //var regionalRequest = _regionalRequestService.GetAllReliefRequistion().FirstOrDefault();


            var regionalRequestDetailToGetCommodityId = new RegionalRequestDetail();
            var reliefRequisitions = new List<ReliefRequisition>();

            //Create Requisiton for Grain

            var commodityId = _commodityService.GetCommoidtyId(regionalRequestDetailToGetCommodityId.GrainName);

            reliefRequisitions.Add(CreateRequisition(regionalRequest, commodityId));


            //Create Requistion for Oil

            commodityId = _commodityService.GetCommoidtyId(regionalRequestDetailToGetCommodityId.OilName);

            reliefRequisitions.Add(CreateRequisition(regionalRequest, commodityId));

            //Create Requistion for pulse

            commodityId = _commodityService.GetCommoidtyId(regionalRequestDetailToGetCommodityId.PulseName);

            reliefRequisitions.Add(CreateRequisition(regionalRequest, commodityId));

            //Create Requistion for Grain

            commodityId = _commodityService.GetCommoidtyId(regionalRequestDetailToGetCommodityId.GrainName);

            reliefRequisitions.Add(CreateRequisition(regionalRequest, commodityId));


           return  reliefRequisitions;
        }
        public  ReliefRequisition CreateRequisition(RegionalRequest regionalRequest, int commodityId)
        {

            var relifRequisition = new ReliefRequisition()
                                       {
                                           //TODO:Please Include Regional Request ID in Requisition 
                                           //RegionalRequestId=regionalRequest.RegionalRequestID
                                           Round = regionalRequest.Round
                                           ,
                                           ProgramID = regionalRequest.ProgramId
                                           ,
                                           CommodityID = commodityId
                                           ,
                                           RequisitionDate = DateTime.Today
                                               //TODO:Please find another way how to specify Requistion No
                                           ,
                                           RequisitionNo = Guid.NewGuid().ToString()


                                       };
            var relifRequistionDetail = (from requestDetail in regionalRequest.RegionalRequestDetails
                                         select new ReliefRequisitionDetail()
                                                    {
                                                        CommodityID = commodityId

                                                        ,
                                                        Amount = requestDetail.Grain
                                                        ,
                                                        Beneficiaries = requestDetail.Beneficiaries
                                                        ,
                                                        FDPID = requestDetail.Fdpid

                                                    }).ToList();
            relifRequisition.ReliefRequisitionDetials = relifRequistionDetail;

            return relifRequisition;

        }

    }
}
