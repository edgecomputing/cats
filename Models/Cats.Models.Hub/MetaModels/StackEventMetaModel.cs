using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class StackEventMetaModel
		{
		
			[Required(ErrorMessage="Stack Event is required")]
    		public Guid StackEventID { get; set; }

			[Required(ErrorMessage="Event Date is required")]
			[DataType(DataType.DateTime)]
    		public DateTime EventDate { get; set; }

			[Required(ErrorMessage="Hub is required")]
    		public Int32 HubID { get; set; }

			[Required(ErrorMessage="Store is required")]
    		public Int32 StoreID { get; set; }

			[Required(ErrorMessage="Stack Event Type is required")]
    		public Int32 StackEventTypeID { get; set; }

			[Required(ErrorMessage="Stack Number is required")]
    		public Int32 StackNumber { get; set; }

			[DataType(DataType.DateTime)]
    		public DateTime FollowUpDate { get; set; }

			[Required(ErrorMessage="Follow Up Performed is required")]
    		public Boolean FollowUpPerformed { get; set; }

			[Required(ErrorMessage="Description is required")]
			[StringLength(4000)]
    		public String Description { get; set; }

			[StringLength(4000)]
    		public String Recommendation { get; set; }

			[Required(ErrorMessage="User Profile is required")]
    		public Int32 UserProfileID { get; set; }

    		public EntityCollection<StackEventType> StackEventType { get; set; }

	   }
}

