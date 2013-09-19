using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Web.Mvc;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class CommodityMetaModel
		{
		
			[Required(ErrorMessage="Commodity is required")]
    		public Int32 CommodityID { get; set; }

			
            [Required(ErrorMessage="Name is required")]
			[StringLength(50)]
            [Remote("IsNameValid", "Commodity", AdditionalFields = "CommodityID", ErrorMessage = "Commodity Name should be Unique")]
    		public String Name { get; set; }

			[StringLength(500)]
    		public String LongName { get; set; }

            [StringLength(50)]
            [UIHint("AmharicTextBox")]
            public String NameAM { get; set; }

			[StringLength(3)]
            [Remote("IsCodeValid","Commodity",AdditionalFields = "CommodityID",ErrorMessage = "Commodity Code should be Unique")]
    		public String CommodityCode { get; set; }

			[Required(ErrorMessage="Commodity Type is required")]
    		public Int32 CommodityTypeID { get; set; }

    		public Int32 ParentID { get; set; }

    		public EntityCollection<DispatchAllocation> DispatchAllocations { get; set; }

    		public EntityCollection<Commodity> Commodity1 { get; set; }

    		public EntityCollection<Commodity> Commodity2 { get; set; }

    		public EntityCollection<DispatchDetail> DispatchDetails { get; set; }

    		public EntityCollection<GiftCertificateDetail> GiftCertificateDetails { get; set; }

    		public EntityCollection<OtherDispatchAllocation> OtherDispatchAllocations { get; set; }

    		public EntityCollection<CommodityType> CommodityType { get; set; }

    		public EntityCollection<ReceiptAllocation> ReceiptAllocations { get; set; }

    		public EntityCollection<ReceiveDetail> ReceiveDetails { get; set; }

    		public EntityCollection<Transaction> Transactions { get; set; }

    		public EntityCollection<Transaction> Transactions1 { get; set; }

	   }
}

