using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.MetaData
{
    public class HRDMetaData
    {
        [Display(Name = "Year")]
        [Required(ErrorMessage = "Please Enter Year")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please Select Season")]
        [Display(Name = "Season")]
        public int SeasonID { get; set; }

         [Display(Name = "Ration")]
        [Required(ErrorMessage = "Please Select Ration")]
        public int RationID { get; set; }

         [Display(Name = "Start Date")]
         [Required(ErrorMessage = "Please Select Start Date")]
         public DateTime StartDate { get; set; }
         [Display(Name = "End Date")]
         [Required(ErrorMessage = "Please Select End Date")]
        public DateTime EndDate { get; set; }

    }
}
