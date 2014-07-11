using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class ErrorLogMetaModel
		{
		
			[Required(ErrorMessage="Error Log is required")]
    		public Guid ErrorLogID { get; set; }

			[Required(ErrorMessage="Partition is required")]
    		public Int32 PartitionID { get; set; }

			[Required(ErrorMessage="Application is required")]
			[StringLength(60)]
    		public String Application { get; set; }

			[Required(ErrorMessage="Host is required")]
			[StringLength(50)]
    		public String Host { get; set; }

			[Required(ErrorMessage="Type is required")]
			[StringLength(100)]
    		public String Type { get; set; }

			[Required(ErrorMessage="Source is required")]
			[StringLength(60)]
    		public String Source { get; set; }

			[Required(ErrorMessage="Message is required")]
			[StringLength(500)]
    		public String Message { get; set; }

			[Required(ErrorMessage="User is required")]
			[StringLength(50)]
    		public String User { get; set; }

			[Required(ErrorMessage="Status Code is required")]
    		public Int32 StatusCode { get; set; }

			[Required(ErrorMessage="Time Utc is required")]
			[DataType(DataType.DateTime)]
    		public DateTime TimeUtc { get; set; }

			[Required(ErrorMessage="Sequence is required")]
    		public Int32 Sequence { get; set; }

			[Required(ErrorMessage="All Xml is required")]
    		public String AllXml { get; set; }

	   }
}

