using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class OtherDispatchAllocationMetaModel
		{
		
			[Required(ErrorMessage="Other Dispatch Allocation is required")]
    		public Guid OtherDispatchAllocationID { get; set; }

			[Required(ErrorMessage="Partition is required")]
    		public Int32 PartitionID { get; set; }

			[Required(ErrorMessage="Agreement Date is required")]
			[DataType(DataType.DateTime)]
    		public DateTime AgreementDate { get; set; }

			[Required(ErrorMessage="Commodity is required")]
    		public Int32 CommodityID { get; set; }

			[Required(ErrorMessage="Estimated Dispatch Date is required")]
			[DataType(DataType.DateTime)]
    		public DateTime EstimatedDispatchDate { get; set; }

			[Required(ErrorMessage="From Hub is required")]
    		public Int32 FromHubID { get; set; }

			[Required(ErrorMessage="To Hub is required")]
    		public Int32 ToHubID { get; set; }

			[Required(ErrorMessage="Program is required")]
    		public Int32 ProgramID { get; set; }

			[Required(ErrorMessage="Unit is required")]
    		public Int32 UnitID { get; set; }

			[Required(ErrorMessage="Quantity In Unit is required")]
    		public Decimal QuantityInUnit { get; set; }

			[Required(ErrorMessage="Quantity In M T is required")]
    		public Decimal QuantityInMT { get; set; }

			[Required(ErrorMessage="Reason is required")]
    		public Int32 ReasonID { get; set; }

			[Required(ErrorMessage="Reference Number is required")]
			[StringLength(50)]
    		public String ReferenceNumber { get; set; }

			[StringLength(4000)]
    		public String Remark { get; set; }

			[Required(ErrorMessage="Shipping Instruction is required")]
    		public Int32 ShippingInstructionID { get; set; }

			[Required(ErrorMessage="Project Code is required")]
    		public Int32 ProjectCodeID { get; set; }

    		public Int32 TransporterID { get; set; }

			[Required(ErrorMessage="Is Closed is required")]
    		public Boolean IsClosed { get; set; }

    		public EntityCollection<Commodity> Commodity { get; set; }

    		public EntityCollection<Dispatch> Dispatches { get; set; }

    		public EntityCollection<Hub> Hub { get; set; }

    		public EntityCollection<Hub> Hub1 { get; set; }

    		public EntityCollection<Program> Program { get; set; }

    		public EntityCollection<ProjectCode> ProjectCode { get; set; }

    		public EntityCollection<ShippingInstruction> ShippingInstruction { get; set; }

    		public EntityCollection<Transporter> Transporter { get; set; }

	   }
}

