using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Areas.EarlyWarning.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;

namespace Cats.ViewModelBinder
{
    public class RequisitionViewModelBinder
    {
        public static ReliefRequisitionViewModel BindReliefRequisitionViewModel(ReliefRequisition reliefRequisition,List<WorkflowStatus> statuses )
        {
            return new ReliefRequisitionViewModel()
            {

                ProgramID = reliefRequisition.ProgramID,
                Program = reliefRequisition.Program.Name,
                Region = reliefRequisition.AdminUnit.Name,
                RequisitionNo = reliefRequisition.RequisitionNo,
                RegionID = reliefRequisition.RegionID,
                RegionalRequestID = reliefRequisition.RegionalRequestID,

                RequestedDateEt = EthiopianDate.GregorianToEthiopian(reliefRequisition.RequestedDate.Value
                ),
                Round = reliefRequisition.Round,
                Status = statuses.Find(t=>t.WorkflowID==(int)WORKFLOW.RELIEF_REQUISITION && t.StatusID== reliefRequisition.Status.Value).Description ,
                RequestedDate = reliefRequisition.RequestedDate.Value,
                StatusID = reliefRequisition.Status,
                RequisitionID = reliefRequisition.RequisitionID,
                CommodityID = reliefRequisition.CommodityID,
                ZoneID = reliefRequisition.ZoneID,
                Zone = reliefRequisition.AdminUnit.Name,
                Commodity = reliefRequisition.Commodity.Name,




            };

        }
        public static ReliefRequisitionDetail BindReliefRequisitionDetail(ReliefRequisitionDetailViewModel reliefRequisitionDetailViewModel)
        {
            return new ReliefRequisitionDetail()
            {


                BenficiaryNo = reliefRequisitionDetailViewModel.BenficiaryNo,
                Amount = reliefRequisitionDetailViewModel.Amount,
                RequisitionID = reliefRequisitionDetailViewModel.RequisitionID,
                RequisitionDetailID = reliefRequisitionDetailViewModel.RequisitionDetailID,
                CommodityID = reliefRequisitionDetailViewModel.CommodityID,
                FDPID = reliefRequisitionDetailViewModel.FDPID,
                DonorID = reliefRequisitionDetailViewModel.DonorID,

            };
        }
        public  static ReliefRequisitionDetailViewModel BindReliefRequisitionDetailViewModel(ReliefRequisitionDetail reliefRequisitionDetail)
        {
            return new ReliefRequisitionDetailViewModel()
            {
                Zone = reliefRequisitionDetail.ReliefRequisition.AdminUnit1.Name,
                Woreda = reliefRequisitionDetail.FDP.AdminUnit.Name,
                FDP = reliefRequisitionDetail.FDP.Name,
                Donor = reliefRequisitionDetail.DonorID.HasValue ? reliefRequisitionDetail.Donor.Name : "",
                Commodity = reliefRequisitionDetail.Commodity.Name,
                BenficiaryNo = reliefRequisitionDetail.BenficiaryNo,
                Amount = reliefRequisitionDetail.Amount,
                RequisitionID = reliefRequisitionDetail.RequisitionID,
                RequisitionDetailID = reliefRequisitionDetail.RequisitionDetailID,
                CommodityID = reliefRequisitionDetail.CommodityID,
                FDPID = reliefRequisitionDetail.FDPID,
                DonorID = reliefRequisitionDetail.DonorID

            };

        }
        public static IEnumerable<ReliefRequisitionViewModel> BindReliefRequisitionListViewModel(IEnumerable<ReliefRequisition> reliefRequisitions,List<WorkflowStatus> statuses )
        {
            return (from requisition in reliefRequisitions
                    select BindReliefRequisitionViewModel(requisition, statuses));
        }
        public static IEnumerable<ReliefRequisitionDetailViewModel> BindReliefRequisitionDetailListViewModel(IEnumerable<ReliefRequisitionDetail> reliefRequisitionDetails)
        {
            return (from requisitionDetail in reliefRequisitionDetails
                    select BindReliefRequisitionDetailViewModel(requisitionDetail));
        }
    }
}