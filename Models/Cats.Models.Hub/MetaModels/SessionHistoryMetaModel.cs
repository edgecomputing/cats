using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class SessionHistoryMetaModel
		{
		
			[Required(ErrorMessage="Session History is required")]
    		public Guid SessionHistoryID { get; set; }

			[Required(ErrorMessage="Partition is required")]
    		public Int32 PartitionID { get; set; }

    		public Int32 UserProfileID { get; set; }

    		public Int32 RoleID { get; set; }

			[Required(ErrorMessage="Login Date is required")]
			[DataType(DataType.DateTime)]
    		public DateTime LoginDate { get; set; }

			[StringLength(50)]
    		public String UserName { get; set; }

			[StringLength(50)]
    		public String Password { get; set; }

			[StringLength(50)]
    		public String WorkstationName { get; set; }

			[StringLength(50)]
    		public String IPAddress { get; set; }

			[StringLength(50)]
    		public String ApplicationName { get; set; }

    		public EntityCollection<Role> Role { get; set; }

    		public EntityCollection<UserProfile> UserProfile { get; set; }

	   }
}

