using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class Transaction
    {
        [Key]
        public System.Guid TransactionID { get; set; }
        public Nullable<int> PartitionID { get; set; }
        public System.Guid? TransactionGroupID { get; set; }
        public int LedgerID { get; set; }
        public Nullable<int> HubOwnerID { get; set; }
        public int? AccountID { get; set; }
        public Nullable<int> HubID { get; set; }
        public Nullable<int> StoreID { get; set; }
        public Nullable<int> Stack { get; set; }
        public Nullable<int> ProjectCodeID { get; set; }
        public Nullable<int> ShippingInstructionID { get; set; }
        public int ProgramID { get; set; }
        public Nullable<int> ParentCommodityID { get; set; }
        public Nullable<int> CommodityID { get; set; }
        public Nullable<int> CommodityGradeID { get; set; }
        public decimal QuantityInMT { get; set; }
        public decimal QuantityInUnit { get; set; }
        public int UnitID { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public virtual Account Account { get; set; }
        public Nullable<int> RegionID { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Round { get; set; }
        public Nullable<int> DonorID { get; set; }
        public Nullable<int> CommoditySourceID { get; set; }
        public Nullable<int> GiftTypeID { get; set; }
        public Nullable<int> PlanId { get; set; } 

        public virtual Commodity Commodity { get; set; }
        public virtual Commodity Commodity1 { get; set; }
        public virtual CommodityGrade CommodityGrade { get; set; }
        public virtual Hub Hub { get; set; }
        public virtual HubOwner HubOwner { get; set; }
        public virtual Ledger Ledger { get; set; }
        public virtual Program Program { get; set; }
        public virtual ProjectCode ProjectCode { get; set; }
        public virtual ShippingInstruction ShippingInstruction { get; set; }
        public virtual Store Store { get; set; }
        public virtual TransactionGroup TransactionGroup { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
