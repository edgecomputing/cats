using System;
using System.Collections.Generic;

namespace Cats.Models.ViewModels.HRD
{
    public class HRDViewModel
    {


        public int HRDID { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
        public string Ration { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PublishedDate { get; set; }
        public string CreatedBy { get; set; }
        public List<HRDDetailViewModel> HRDDetails { get; set; }

       

        
    }
}
