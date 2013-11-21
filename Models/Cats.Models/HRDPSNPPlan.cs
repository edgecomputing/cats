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
        public int? SeasonID { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [Display(Name="Program")]
        public int ProgramID { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        [Display(Name="Region")]
        public int RegionID { get; set; }


        [Display(Name = "Donor")]
        public int? DonorID { get; set; }

        public int? RationID { get; set; }
        public int? Round { get; set; }
         [Required]
        public int PlanID { get; set; }

    }
}