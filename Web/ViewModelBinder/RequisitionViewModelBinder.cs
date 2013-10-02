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
        public static ReliefRequisitionViewModel BindReliefRequisitionViewModel(ReliefRequisition reliefRequisition, List<WorkflowStatus> statuses, string datePref)
        {
            var requisition = new ReliefRequisitionViewModel();


            requisition.ProgramID = reliefRequisition.ProgramID;
            requisition.Program = reliefRequisition.Program.Name;
            requisition.Region = reliefRequisition.AdminUnit.Name;
            requisition.RequisitionNo = reliefRequisition.RequisitionNo;
            requisition.RegionID = reliefRequisition.RegionID;
            requisition.RegionalRequestID = reliefRequisition.RegionalRequestID;
            if (reliefRequisition.RequestedDate.HasValue)
                requisition.RequestedDateEt = reliefRequisition.RequestedDate.Value.ToCTSPreferedDateFormat(datePref);
            //);
            requisition.Round = reliefRequisition.Round;
            requisition.Status = statuses.Find(t => t.WorkflowID == (int)WORKFLOW.RELIEF_REQUISITION && t.StatusID == reliefRequisition.Status.Value).Description;
            requisition.RequestedDate = reliefRequisition.RequestedDate.Value;
            requisition.StatusID = reliefRequisition.Status;
            requisition.RequisitionID = reliefRequisition.RequisitionID;
            requisition.CommodityID = reliefRequisition.CommodityID;
            requisition.ZoneID = reliefRequisition.ZoneID;
            requisition.Zone = reliefRequisition.AdminUnit1.Name;
            requisition.Commodity = reliefRequisition.Commodity.Name;
            requisition.Month = RequestHelper.MonthName(reliefRequisition.Month);
            //requisition.MonthRound;
            //reliefRequisition.
            return requisition;


        }

        //private static string match(int? r, ReliefRequisition reliefRequisition)
        //{
        //    string month = "";

        //    if (reliefRequisition.ProgramID == 2)
        //    {
        //        switch (r)
        //        {
        //            case 1:
        //                month = "Meskerem";
        //                break;
        //            case 2:
        //                month = "Tikmet";
        //                break;
        //            case 3:
        //                month = "Hidar";
        //                break;
        //            case 9:
        //                month = "Ginbot";
        //                break;
        //        }
        //    }

        //    return month;
        //}

        public static List<ReliefRequisitionViewModel> BindRequisitionViewModel(List<ReliefRequisition> reliefRequisitions)
        {
            var reliefRequisitionViewModels = new List<ReliefRequisitionViewModel>();

            foreach (var reliefRequisition in reliefRequisitions)
            {
                var reliefRequisitionViewModel = new ReliefRequisitionViewModel();
                reliefRequisitionViewModel.RequisitionNo = reliefRequisition.RequisitionNo;
                reliefRequisitionViewModel.RequestedDate = (DateTime)reliefRequisition.RequestedDate;
                reliefRequisitionViewModel.Status = reliefRequisition.Status.ToString();
                reliefRequisitionViewModels.Add(reliefRequisitionViewModel);

            }
            return reliefRequisitionViewModels;
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
        public static ReliefRequisitionDetailViewModel BindReliefRequisitionDetailViewModel(ReliefRequisitionDetail reliefRequisitionDetail)
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
        public static IEnumerable<ReliefRequisitionViewModel> BindReliefRequisitionListViewModel(IEnumerable<ReliefRequisition> reliefRequisitions, List<WorkflowStatus> statuses, string datePref)
        {
            return (from requisition in reliefRequisitions
                    select BindReliefRequisitionViewModel(requisition, statuses, datePref));
        }
        public static IEnumerable<ReliefRequisitionDetailViewModel> BindReliefRequisitionDetailListViewModel(IEnumerable<ReliefRequisitionDetail> reliefRequisitionDetails)
        {
            return (from requisitionDetail in reliefRequisitionDetails
                    select BindReliefRequisitionDetailViewModel(requisitionDetail));
        }
    }
}