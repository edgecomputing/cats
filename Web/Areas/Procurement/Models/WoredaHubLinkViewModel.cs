using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class WoredaHubLinkViewModel
    {
        public int WoredaHubLinkID { get; set; }

        public int WoredaHubID { get; set; }

        public int WoredaID { get; set; }

        [Display(Name = "Woreda")]
        public string Woreda { get; set; }

        public int HubID { get; set; }

        [Display(Name = "Hub")]
        public string Hub { get; set; }
    }
}