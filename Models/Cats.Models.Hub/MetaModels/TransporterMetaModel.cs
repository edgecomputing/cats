using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Web.Mvc;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class TransporterMetaModel
		{
		
			[Required(ErrorMessage="Transporter is required")]
    		public Int32 TransporterID { get; set; }

			[Required(ErrorMessage="Name is required")]
			[StringLength(50)]
            [Remote("IsNameValid","Transporter",AdditionalFields = "TransporterID",ErrorMessage = "Transport Name should be Unique")]
    		public String Name { get; set; }

            [Required(ErrorMessage = "Amharic Name is required")]
            [StringLength(50, ErrorMessage = "Name should be less than 50 characters")]
            [DisplayName("Name (Amharic)")]
            [UIHint("AmharicTextBox")]
            public String NameAM { get; set; }

			[StringLength(50)]
    		public String LongName { get; set; }

			[StringLength(50)]
    		public String BiddingSystemID { get; set; }

    		public EntityCollection<Dispatch> Dispatches { get; set; }

    		public EntityCollection<DispatchAllocation> DispatchAllocations { get; set; }

    		public EntityCollection<OtherDispatchAllocation> OtherDispatchAllocations { get; set; }

    		public EntityCollection<Receive> Receives { get; set; }

	   }
}

