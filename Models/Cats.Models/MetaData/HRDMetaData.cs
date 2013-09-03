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
        public int Year { get; set; }

        [Required(ErrorMessage = "Please Select Season")]
        [Display(Name = "Season")]
        public int SeasonID { get; set; }

         [Display(Name = "Ration")]
        [Required(ErrorMessage = "Please Select Ration")]
        public int RationID { get; set; }

       

    }
}
