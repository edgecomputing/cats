using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class GiftCertificateMetaModel
		{
		
			[Required(ErrorMessage="Gift Certificate is required")]
    		public Int32 GiftCertificateID { get; set; }

			[Required(ErrorMessage="Gift Date is required")]
			[DataType(DataType.DateTime)]
    		public DateTime GiftDate { get; set; }

			[Required(ErrorMessage="Donor is required")]
    		public Int32 DonorID { get; set; }

			[Required(ErrorMessage="S I Number is required")]
			[StringLength(50)]
    		public String SINumber { get; set; }

			[Required(ErrorMessage="Reference No is required")]
			[StringLength(50)]
    		public String ReferenceNo { get; set; }

			[StringLength(50)]
    		public String Vessel { get; set; }

			[Required(ErrorMessage="E T A is required")]
			[DataType(DataType.DateTime)]
    		public DateTime ETA { get; set; }

			[Required(ErrorMessage="Is Printed is required")]
    		public Boolean IsPrinted { get; set; }

			[Required(ErrorMessage="Program is required")]
    		public Int32 ProgramID { get; set; }

			[Required(ErrorMessage="D Mode Of Transport is required")]
    		public Int32 DModeOfTransport { get; set; }

			[StringLength(50)]
    		public String PortName { get; set; }

    		public EntityCollection<Detail> Detail { get; set; }

    		public EntityCollection<Donor> Donor { get; set; }

    		public EntityCollection<Program> Program { get; set; }

    		public EntityCollection<GiftCertificateDetail> GiftCertificateDetails { get; set; }

	   }
}

