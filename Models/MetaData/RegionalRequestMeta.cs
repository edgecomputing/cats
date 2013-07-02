using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class RegionalRequestMeta
    {
       
        public int RegionalRequestID { get; set; }

        [Required (ErrorMessage="Please Select Region")]
        [Display (Name="Region")]
        public int RegionID { get; set; }

        [Required(ErrorMessage = "Please Select Program")]
        [Display(Name="Program")]
        public int ProgramId { get; set; }
        [Required(ErrorMessage="Please Select Round")]
        public int Round { get; set; }

        [Required(ErrorMessage = "Please Enter Requisition Date")]
        public DateTime RequistionDate { get; set; }

        [Required(ErrorMessage = "Please Select Year")]
        public int Year { get; set; }
        [Required (ErrorMessage="Please Enter Reference Number")]
        public String ReferenceNumber { get; set; }
        public string Remark { get; set; }

      
    }
}
