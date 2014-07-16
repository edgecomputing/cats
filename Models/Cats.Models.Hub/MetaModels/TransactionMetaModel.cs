using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class TransactionMetaModel
		{
		
			[Required(ErrorMessage="Transaction is required")]
    		public Guid TransactionID { get; set; }

            ////[Required(ErrorMessage="Partition is required")]
            //public Int32? PartitionId { get; set; }

    		public Guid? TransactionGroupID { get; set; }

			[Required(ErrorMessage="Ledger is required")]
    		public Int32 LedgerID { get; set; }

			//[Required(ErrorMessage="Hub Owner is required")]
    		public Int32? HubOwnerID { get; set; }

			//[Required(ErrorMessage="Account is required")]
    		public Int32? AccountID { get; set; }

			//[Required(ErrorMessage="Hub is required")]
    		public Int32? HubID { get; set; }

			//[Required(ErrorMessage="Store is required")]
    		public Int32? StoreID { get; set; }

    		public Int32? Stack { get; set; }

			//[Required(ErrorMessage="Project Code is required")]
    		public Int32? ProjectCodeID { get; set; }

			//[Required(ErrorMessage="Shipping Instruction is required")]
    		public Int32? ShippingInstructionID { get; set; }

			[Required(ErrorMessage="Program is required")]
    		public Int32 ProgramID { get; set; }

			//[Required(ErrorMessage="Parent Commodity is required")]
    		public Int32? ParentCommodityID { get; set; }

			//[Required(ErrorMessage="Commodity is required")]
    		public Int32? CommodityID { get; set; }

    		public Int32? CommodityGradeID { get; set; }

			[Required(ErrorMessage="Quantity In M T is required")]
    		public Decimal QuantityInMT { get; set; }

			[Required(ErrorMessage="Quantity In Unit is required")]
    		public Decimal QuantityInUnit { get; set; }

			[Required(ErrorMessage="Unit is required")]
    		public Int32 UnitID { get; set; }

			[Required(ErrorMessage="Transaction Date is required")]
			[DataType(DataType.DateTime)]
    		public DateTime TransactionDate { get; set; }

    		public EntityCollection<Account> Account { get; set; }

    		public EntityCollection<Commodity> Commodity { get; set; }

    		public EntityCollection<Commodity> Commodity1 { get; set; }

    		public EntityCollection<CommodityGrade> CommodityGrade { get; set; }

    		public EntityCollection<Hub> Hub { get; set; }

    		public EntityCollection<HubOwner> HubOwner { get; set; }

    		public EntityCollection<Ledger> Ledger { get; set; }

    		public EntityCollection<Program> Program { get; set; }

    		public EntityCollection<ProjectCode> ProjectCode { get; set; }

    		public EntityCollection<ShippingInstruction> ShippingInstruction { get; set; }

    		public EntityCollection<Store> Store { get; set; }

    		public EntityCollection<TransactionGroup> TransactionGroup { get; set; }

    		public EntityCollection<Unit> Unit { get; set; }

	   }
}

