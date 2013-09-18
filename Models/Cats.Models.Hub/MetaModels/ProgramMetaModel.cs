using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class ProgramMetaModel
		{
		
			[Required(ErrorMessage="Program is required")]
    		public Int32 ProgramID { get; set; }

			[Required(ErrorMessage="Name is required")]
			[StringLength(50)]
    		public String Name { get; set; }

			[StringLength(50)]
    		public String Description { get; set; }

			[StringLength(500)]
    		public String LongName { get; set; }

    		public EntityCollection<DispatchAllocation> DispatchAllocations { get; set; }

    		public EntityCollection<GiftCertificate> GiftCertificates { get; set; }

    		public EntityCollection<OtherDispatchAllocation> OtherDispatchAllocations { get; set; }

    		public EntityCollection<ReceiptAllocation> ReceiptAllocations { get; set; }

    		public EntityCollection<Transaction> Transactions { get; set; }

	   }
}

