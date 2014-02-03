using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Web.Mvc;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class DonorMetaModel
		{
		
			[Required(ErrorMessage="Donor is required")]
    		public Int32 DonorID { get; set; }

			[Required(ErrorMessage="Name is required")]
			[StringLength(50)]
    		public String Name { get; set; }

			[StringLength(3)]
            [Remote("IsCodeValid","Donor",AdditionalFields = "DonorID",ErrorMessage = "Donor code should be unique")]
    		public String DonorCode { get; set; }

			[Required(ErrorMessage="Is Responsible Donor is required")]
    		public Boolean IsResponsibleDonor { get; set; }

			[Required(ErrorMessage="Is Source Donor is required")]
    		public Boolean IsSourceDonor { get; set; }

			[StringLength(500)]
    		public String LongName { get; set; }

    		public EntityCollection<GiftCertificate> GiftCertificates { get; set; }

    		public EntityCollection<ReceiptAllocation> ReceiptAllocations { get; set; }

            //public EntityCollection<Receive> Receives { get; set; }

            //public EntityCollection<Receive> Receives1 { get; set; }

	   }
}

