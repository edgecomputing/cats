using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class DispatchAllocationMetaModel
		{
		
			[Required(ErrorMessage="Dispatch Allocation is required")]
			public Guid DispatchAllocationID { get; set; }

			[Required(ErrorMessage="Partition is required")]
			public Int32 PartitionID { get; set; }

			[Required(ErrorMessage="Hub is required")]
			public Int32 HubID { get; set; }

			public Int32 Year { get; set; }

			public Int32 Month { get; set; }

			public Int32 Round { get; set; }

			public Int32 DonorID { get; set; }

			public Int32 ProgramID { get; set; }

			[Required(ErrorMessage="Commodity is required")]
			public Int32 CommodityID { get; set; }

			[Required(ErrorMessage="Requisition No is required")]
			[StringLength(50)]
			public String RequisitionNo { get; set; }

			[StringLength(50)]
			public String BidRefNo { get; set; }

			public Int32 Beneficiery { get; set; }

			[Required(ErrorMessage="Amount is required")]
			public Decimal Amount { get; set; }

			[Required(ErrorMessage="Unit is required")]
			public Int32 Unit { get; set; }

			public Int32 TransporterID { get; set; }

			[Required(ErrorMessage="FDP is required")]
			public Int32 FDPID { get; set; }

			public Int32 ShippingInstructionID { get; set; }

			public Int32 ProjectCodeID { get; set; }

			[Required(ErrorMessage="Is Closed is required")]
			public Boolean IsClosed { get; set; }

			public EntityCollection<Commodity> Commodity { get; set; }

			public EntityCollection<Dispatch> Dispatches { get; set; }

			public EntityCollection<FDP> FDP { get; set; }

			public EntityCollection<Program> Program { get; set; }

			public EntityCollection<Hub> Hub { get; set; }

			public EntityCollection<ProjectCode> ProjectCode { get; set; }

			public EntityCollection<ShippingInstruction> ShippingInstruction { get; set; }

			public EntityCollection<Transporter> Transporter { get; set; }

	   }
}

