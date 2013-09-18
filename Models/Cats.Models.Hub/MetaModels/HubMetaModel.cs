using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class HubMetaModel
		{
		
			[Required(ErrorMessage="Hub is required")]
    		public Int32 HubID { get; set; }

			[Required(ErrorMessage="Name is required")]
			[StringLength(50)]
    		public String Name { get; set; }

			[Required(ErrorMessage="Hub Owner is required")]
    		public Int32 HubOwnerID { get; set; }

    		public EntityCollection<Dispatch> Dispatches { get; set; }

    		public EntityCollection<DispatchAllocation> DispatchAllocations { get; set; }

    		public EntityCollection<OtherDispatchAllocation> OtherDispatchAllocations { get; set; }

    		public EntityCollection<OtherDispatchAllocation> OtherDispatchAllocations1 { get; set; }

    		public EntityCollection<ReceiptAllocation> ReceiptAllocations { get; set; }

    		public EntityCollection<ReceiptAllocation> ReceiptAllocations1 { get; set; }

    		public EntityCollection<Receive> Receives { get; set; }

    		public EntityCollection<Store> Stores { get; set; }

    		public EntityCollection<Transaction> Transactions { get; set; }

    		public EntityCollection<UserHub> UserHubs { get; set; }

    		public EntityCollection<HubOwner> HubOwner { get; set; }

    		public EntityCollection<HubSettingValue> HubSettingValues { get; set; }

	   }
}

