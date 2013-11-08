using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class GiftCertificateDetailMetaModel
		{
		
			[Required(ErrorMessage="Gift Certificate Detail is required")]
    		public Int32 GiftCertificateDetailID { get; set; }

			[Required(ErrorMessage="Partition is required")]
    		public Int32 PartitionID { get; set; }

			[Required(ErrorMessage="Transaction Group is required")]
    		public Int32 TransactionGroupID { get; set; }

			[Required(ErrorMessage="Gift Certificate is required")]
    		public Int32 GiftCertificateID { get; set; }

			[Required(ErrorMessage="Commodity is required")]
    		public Int32 CommodityID { get; set; }

			[Required(ErrorMessage="Weight In M T is required")]
    		public Decimal WeightInMT { get; set; }

			[StringLength(50)]
    		public String BillOfLoading { get; set; }

			[Required(ErrorMessage="Account Number is required")]
    		public Int32 AccountNumber { get; set; }

			[Required(ErrorMessage="Estimated Price is required")]
    		public Decimal EstimatedPrice { get; set; }

			[Required(ErrorMessage="Estimated Tax is required")]
    		public Decimal EstimatedTax { get; set; }

			[Required(ErrorMessage="Year Purchased is required")]
    		public Int32 YearPurchased { get; set; }

			[Required(ErrorMessage="D Fund Source is required")]
    		public Int32 DFundSourceID { get; set; }

			[Required(ErrorMessage="D Currency is required")]
    		public Int32 DCurrencyID { get; set; }

			[Required(ErrorMessage="D Budget Type is required")]
    		public Int32 DBudgetTypeID { get; set; }

			[DataType(DataType.DateTime)]
    		public DateTime ExpiryDate { get; set; }

    		public EntityCollection<Commodity> Commodity { get; set; }

    		public EntityCollection<Detail> Detail { get; set; }

    		public EntityCollection<Detail> Detail1 { get; set; }

    		public EntityCollection<Detail> Detail2 { get; set; }

    		public EntityCollection<GiftCertificate> GiftCertificate { get; set; }

    		public EntityCollection<ReceiptAllocation> ReceiptAllocations { get; set; }

	   }
}

