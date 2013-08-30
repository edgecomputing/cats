using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class HRDPSNPPlan
    {

        public int SeasonID { get; set; }
        [Required(ErrorMessage = "Year can't be empty.")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Program can't be empty.")]
        [Display(Name="Program")]
        public int ProgramID { get; set; }
        [Required(ErrorMessage = "Month can't be empty.")]
        public int Month { get; set; }
        [Required(ErrorMessage = "Region can't be empty")]
        [Display(Name="Region")]
        public int RegionID { get; set; }


    }
}
