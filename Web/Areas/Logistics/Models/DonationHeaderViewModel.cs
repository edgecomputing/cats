using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class DonationHeaderViewModel
    {
        public int DonationHeaderPlanID { get; set; }
        public int ShippingInstructionId { get; set; }
        public string SINumber { get; set; }
        public Nullable<int> GiftCertificateID { get; set; }
        public int CommodityID { get; set; }
        public string CommodityName { get; set; }   
        public int DonorID { get; set; }
        public string DonorName { get; set; }
        public int ProgramID { get; set; }
        public string ProgramName { get; set; }
        public System.DateTime ETA { get; set; }
        public string Vessel { get; set; }
        public string ReferenceNo { get; set; }
        public Nullable<int> ModeOfTransport { get; set; }
        public Nullable<System.Guid> TransactionGroupID { get; set; }
        public Nullable<bool> IsCommited { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public string strEnteredBy { get; set; }
        public string ProcessedBy { get; set; }
        public Nullable<System.DateTime> AllocationDate { get; set; }
        public string DateOfAllocation { get; set; }
        public string StrETA { get; set; }
        public string Remark { get; set; }
    }
}