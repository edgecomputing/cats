using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class SMSMetaModel
		{
		
			[Required(ErrorMessage="S M S is required")]
    		public Int32 SMSID { get; set; }

			[Required(ErrorMessage="In Out Ind is required")]
			[StringLength(1)]
    		public String InOutInd { get; set; }

			[Required(ErrorMessage="Mobile Number is required")]
			[StringLength(30)]
    		public String MobileNumber { get; set; }

			[Required(ErrorMessage="Text is required")]
			[StringLength(500)]
    		public String Text { get; set; }

			[DataType(DataType.DateTime)]
    		public DateTime RequestDate { get; set; }

			[DataType(DataType.DateTime)]
    		public DateTime SendAfterDate { get; set; }

			[DataType(DataType.DateTime)]
    		public DateTime QueuedDate { get; set; }

			[DataType(DataType.DateTime)]
    		public DateTime SentDate { get; set; }

			[Required(ErrorMessage="Status is required")]
			[StringLength(10)]
    		public String Status { get; set; }

			[Required(ErrorMessage="Status Date is required")]
			[DataType(DataType.DateTime)]
    		public DateTime StatusDate { get; set; }

			[Required(ErrorMessage="Attempts is required")]
    		public Int32 Attempts { get; set; }

			[DataType(DataType.DateTime)]
    		public DateTime LastAttemptDate { get; set; }

			[StringLength(30)]
    		public String EventTag { get; set; }

	   }
}

