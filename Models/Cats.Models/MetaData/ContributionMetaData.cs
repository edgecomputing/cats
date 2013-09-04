using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.MetaData
{
   public class ContributionMetaData
    {
        [Required(ErrorMessage = "Please Select HRD")]
        public int HRDID { get; set; }

        [Required(ErrorMessage = "Please Select Donor")]
        public int DonorID { get; set; }

        [Required(ErrorMessage = "Please Select Year")]
        public int Year { get; set; }
    }
}
