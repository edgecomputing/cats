using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Cats.Models;

namespace Cats.Areas.Procurement.Models
{
    public class WoredaHubViewModel
    {
        
        public int WoredaHubID { get; set; }

        public int HRDID { get; set; }

        public string HRD { get; set; }

        public int PlanID { get; set; }

        public string Plan { get; set; }
        
        [Display(Name = "Name")]
        public string WoredaHubName { get; set; }

        
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        
        [Display(Name = "Status")]
        public int Status { get; set; }
    }
}