using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class ForgetPasswordRequestMetaModel
		{
		
			[Required(ErrorMessage="Forget Password Request is required")]
    		public Int32 ForgetPasswordRequestID { get; set; }

			[Required(ErrorMessage="User Profile is required")]
    		public Int32 UserProfileID { get; set; }

			[Required(ErrorMessage="Generated Date is required")]
			[DataType(DataType.DateTime)]
    		public DateTime GeneratedDate { get; set; }

			[Required(ErrorMessage="Expiery Date is required")]
			[DataType(DataType.DateTime)]
    		public DateTime ExpieryDate { get; set; }

			[Required(ErrorMessage="Completed is required")]
    		public Boolean Completed { get; set; }

			[Required(ErrorMessage="Request Key is required")]
			[StringLength(50)]
    		public String RequestKey { get; set; }

	   }
}

