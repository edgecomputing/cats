using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels;

namespace Cats.ViewModelBinder
{
    public class DispatchReportViewModelBinder
    {
        public static IEnumerable<DispatchReportViewModel> ReturnDispatchReportViewModel(List<VWDispatchCommodity> dispatchReportViewModels )
        {
            return dispatchReportViewModels.Select(s => new DispatchReportViewModel()
                                                            {
            //DispatchedAmountInMT = s.DispatchedAmountInMT,
            //DispatchedAmountInUnit=s.DispatchedAmountInUnit,
            //LedgerID = s.LedgerID,
            //Amount=s.Amount,
            //RemainingInMT=s.RemainingInMT,
            //RemainingInUnit = s.RemainingInUnit,
            //FDPID = s.FDPID,
                                                                Hub = s.Hub,
                                                                HubId = s.HubId
            //ProjectCode = s.ProjectCode,
            //ShippingInstruction = s.ShippingInstruction,
            //FDPName = s.FDPName,
            //AdminUnitTypeID = s.AdminUnitTypeID,
            //ParentID = s.ParentID,
            //IsClosed = s.IsClosed,
            //Donor = s.Donor,
            //CommodityID = s.CommodityID,
            //Commodity=s.Commodity,
            //RequisitionNo=s.RequisitionNo,
            //BidRefNo = s.BidRefNo,
            //ContractStartDate = s.ContractStartDate,
            //ContractEndDate = s.ContractEndDate,
            // Beneficiery = s.Beneficiery,
            //Unit= s.Unit,
            //TransporterID = s.TransporterID,
            //Name = s.Name,
            //Round = s.Round,
            //Month=s.Month,
            //Year = s.Year,
            //DonorID = s.DonorID,
            //ProgramID = s.ProgramID,
            //ShippingInstructionID = s.ShippingInstructionID,
            //ProjectCodeID = s.ProjectCodeID,
            //DispatchDate = s.DispatchDate,
            //CreatedDate = s.CreatedDate,
            //DispatchedByStoreMan = s.DispatchedByStoreMan,
            //GIN = s.GIN,
            //DispatchID = s.DispatchID,
            //Zone = s.Zone,
            //Region = s.Region,
            //RegionId = s.RegionId,
            //ZoneId =s.ZoneId
        
                                                            });
        }
    }
}