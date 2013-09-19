using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class DetailMetaModel
		{
		
			[Required(ErrorMessage="Detail is required")]
    		public Int32 DetailID { get; set; }

			[Required(ErrorMessage="Name is required")]
			[StringLength(50)]
    		public String Name { get; set; }

			[StringLength(500)]
    		public String Description { get; set; }

			[Required(ErrorMessage="Master is required")]
    		public Int32 MasterID { get; set; }

			[Required(ErrorMessage="Sort Order is required")]
    		public Int32 SortOrder { get; set; }

    		public EntityCollection<Master> Master { get; set; }

    		public EntityCollection<GiftCertificate> GiftCertificates { get; set; }

    		public EntityCollection<GiftCertificateDetail> GiftCertificateDetails { get; set; }

    		public EntityCollection<GiftCertificateDetail> GiftCertificateDetails1 { get; set; }

    		public EntityCollection<GiftCertificateDetail> GiftCertificateDetails2 { get; set; }

    		public EntityCollection<InternalMovement> InternalMovements { get; set; }

	   }
}

