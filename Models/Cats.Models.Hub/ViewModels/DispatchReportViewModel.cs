using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Hubs.ViewModels
{
    public class DispatchReportViewModel
    {
        public Nullable<decimal> DispatchedAmountInMT { get; set; }
        public Nullable<decimal> DispatchedAmountInUnit { get; set; }
        public Nullable<int> LedgerID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> RemainingInMT { get; set; }
        public Nullable<decimal> RemainingInUnit { get; set; }
        public Nullable<int> FDPID { get; set; }
        public string Hub { get; set; }
        public Nullable<int> HubId { get; set; }
        public string ProjectCode { get; set; }
        public string ShippingInstruction { get; set; }
        public string FDPName { get; set; }
        public Nullable<int> AdminUnitTypeID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public Nullable<bool> IsClosed { get; set; }
        public string Donor { get; set; }
        public Nullable<int> CommodityID { get; set; }
        public string Commodity { get; set; }
        public string RequisitionNo { get; set; }
        public string BidRefNo { get; set; }
        public Nullable<System.DateTime> ContractStartDate { get; set; }
        public Nullable<System.DateTime> ContractEndDate { get; set; }
        public Nullable<int> Beneficiery { get; set; }
        public Nullable<int> Unit { get; set; }
        public Nullable<int> TransporterID { get; set; }
        public string Name { get; set; }
        public Nullable<int> Round { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> DonorID { get; set; }
        public Nullable<int> ProgramID { get; set; }
        public Nullable<int> ShippingInstructionID { get; set; }
        public Nullable<int> ProjectCodeID { get; set; }
        public Nullable<System.DateTime> DispatchDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string DispatchedByStoreMan { get; set; }
        public string GIN { get; set; }
        public Nullable<System.Guid> DispatchID { get; set; }
        public string Zone { get; set; }
        public string Region { get; set; }
        public Nullable<int> RegionId { get; set; }
        public Nullable<int> ZoneId { get; set; }
    }
}
