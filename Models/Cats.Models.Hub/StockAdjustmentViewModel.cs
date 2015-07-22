using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Hubs
{
   public class StockAdjustmentViewModel
    {
       
       
        public System.Guid? TransactionGroupID { get; set; }
        public int LedgerID { get; set; }
        public Nullable<int> HubOwnerID { get; set; }
        public int? AccountID { get; set; }
        public Nullable<int> HubID { get; set; }
       public string HubName { get; set; }
        public Nullable<int> StoreID { get; set; }
        public Nullable<int> Stack { get; set; }
        public Nullable<int> ProjectCodeID { get; set; }
        public Nullable<int> ShippingInstructionID { get; set; }
       public string SINumber { get; set; }
    
        public int ProgramID { get; set; }
       public string ProgramName { get; set; }
        public Nullable<int> FDPID { get; set; }
        public Nullable<int> ParentCommodityID { get; set; }
        public Nullable<int> CommodityID { get; set; }
       public string commodityName { get; set; }
        public Nullable<int> CommodityChildID { get; set; }
        public Nullable<int> CommodityGradeID { get; set; }
        public decimal QuantityInMT { get; set; }
        public decimal QuantityInUnit { get; set; }
        public int? UnitID { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public virtual Account Account { get; set; }
        public Nullable<int> RegionID { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Round { get; set; }
        public Nullable<int> DonorID { get; set; }
        public Nullable<int> CommoditySourceID { get; set; }
        public Nullable<int> GiftTypeID { get; set; }
        public Nullable<int> PlanId { get; set; }
        public bool IsFalseGRN { get; set; }
    }
}
