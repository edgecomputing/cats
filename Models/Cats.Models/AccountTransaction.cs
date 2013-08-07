using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class AccountTransaction
    {

        //AccountTransactionID
        [Display(Name = "AccountTransactionID")]
        public Guid AccountTransactionID { get; set; }

        //TransactionGroupID
        [Display(Name = "TransactionGroupID")]
        public Guid TransactionGroupID { get; set; }

        //PartitionID
        [Display(Name = "PartitionID")]
        public int PartitionID { get; set; }

        //LedgerID
        [Display(Name = "LedgerID")]
        public int LedgerID { get; set; }

        //HubOwnerID
        [Display(Name = "HubOwnerID")]
        public int HubOwnerID { get; set; }

        //AccountID
        [Display(Name = "AccountID")]
        public int AccountID { get; set; }

        //HubID
        [Display(Name = "HubID")]
        public int HubID { get; set; }

        //StoreID
        [Display(Name = "StoreID")]
        public int StoreID { get; set; }

        //Stack
        [Display(Name = "Stack")]
        public int Stack { get; set; }

        //ProjectCodeID
        [Display(Name = "ProjectCodeID")]
        public int ProjectCodeID { get; set; }

        //ShippingInstructionID
        [Display(Name = "ShippingInstructionID")]
        public int ShippingInstructionID { get; set; }

        //ProgramID
        [Display(Name = "ProgramID")]
        public int ProgramID { get; set; }

        //ParentCommodityID
        [Display(Name = "ParentCommodityID")]
        public int ParentCommodityID { get; set; }

        //CommodityID
        [Display(Name = "CommodityID")]
        public int CommodityID { get; set; }

        //CommodityGradeID
        [Display(Name = "CommodityGradeID")]
        public int CommodityGradeID { get; set; }

        //QuantityInMT
        [Display(Name = "QuantityInMT")]
        public Decimal QuantityInMT { get; set; }

        //QuantityInUnit
        [Display(Name = "QuantityInUnit")]
        public Decimal QuantityInUnit { get; set; }

        //UnitID
        [Display(Name = "UnitID")]
        public int UnitID { get; set; }

        //TransactionDate
        [Display(Name = "TransactionDate")]
        public DateTime TransactionDate { get; set; }

        //RegionID
        [Display(Name = "RegionID")]
        public int RegionID { get; set; }

        //Month
        [Display(Name = "Month")]
        public int Month { get; set; }

        //Round
        [Display(Name = "Round")]
        public int Round { get; set; }
    }
}