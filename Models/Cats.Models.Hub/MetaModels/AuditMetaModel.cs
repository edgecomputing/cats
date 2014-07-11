using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class AuditMetaModel
		{
		
			[Required(ErrorMessage="Audit is required")]
    		public Guid AuditID { get; set; }

    		public Int32 HubID { get; set; }

			[Required(ErrorMessage="Partition is required")]
    		public Int32 PartitionID { get; set; }

			[Required(ErrorMessage="Login is required")]
    		public Int32 LoginID { get; set; }

			[Required(ErrorMessage="Date Time is required")]
			[DataType(DataType.DateTime)]
    		public DateTime DateTime { get; set; }

			[Required(ErrorMessage="Action is required")]
			[StringLength(1)]
    		public String Action { get; set; }

			[Required(ErrorMessage="Table Name is required")]
			[StringLength(30)]
    		public String TableName { get; set; }

			[StringLength(50)]
    		public String PrimaryKey { get; set; }

			[StringLength(3000)]
    		public String ColumnName { get; set; }

    		public String NewValue { get; set; }

    		public String OldValue { get; set; }

	   }
}

