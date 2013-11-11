using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class PeriodMetaModel
		{
		
			[Required(ErrorMessage="Period is required")]
    		public Int32 PeriodID { get; set; }

    		public Int32 Year { get; set; }

    		public Int32 Month { get; set; }

	   }
}

