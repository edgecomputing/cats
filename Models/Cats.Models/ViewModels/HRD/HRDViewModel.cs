using System;
using System.Collections.Generic;

namespace Cats.Models.ViewModels.HRD
{
    public class HRDViewModel
    {


        public int HRDID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int RationID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PublishedDate { get; set; }
        public List<HRDDetailViewModel> HRDDetails { get; set; }

       

        
    }
}
