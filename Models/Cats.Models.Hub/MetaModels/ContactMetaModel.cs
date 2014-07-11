using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class ContactMetaModel
		{
		
			[Required(ErrorMessage="Contact is required")]
    		public Int32 ContactID { get; set; }

			[Required(ErrorMessage="First Name is required")]
			[StringLength(350)]
    		public String FirstName { get; set; }

			[Required(ErrorMessage="Last Name is required")]
			[StringLength(350)]
    		public String LastName { get; set; }

			[Required(ErrorMessage="Phone No is required")]
			[StringLength(10)]
			[DataType(DataType.PhoneNumber)]
    		public String PhoneNo { get; set; }

			[Required(ErrorMessage="F D P is required")]
    		public Int32 FDPID { get; set; }

    		public EntityCollection<FDP> FDP { get; set; }
	   }
}