using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.MetaData
{
   public class PlanMetaData
    {
        [Display(Name = "Plan Name")]
        [Required(ErrorMessage = "Please Enter Plan Name ")]
         public string PlanName { get; set; }

        [Required(ErrorMessage = "Please Select Start Date")]
        public DateTime StartDate { get; set; }

        
        [Required(ErrorMessage = "Please Select End Date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Please Select Program")]
        public int ProgramID { get; set; }
  
    }
}
