using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            var reliefRequistion = CreateRequistionFromRequest(id);
            
            var reliefView = View(reliefRequistion);
            return reliefView;
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
